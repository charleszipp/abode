# Noisy Neighbors & Multi-tenancy

- [Isolation in Azure](https://docs.microsoft.com/en-us/azure/security/fundamentals/isolation-choices) - ~15 min, focus on reading Tenant Isolation, Azure Tenancy, Azure role-based access control (Azure RBAC), and Storage Isolation 
- [Custom roles in RBAC](https://docs.microsoft.com/en-us/azure/role-based-access-control/custom-roles) - ~20 min including following steps

## Overview
Imagine you have a neighbor who plays the Gilligan’s Island theme song 24/7. You might have trouble concentrating, or even doing anything at all. This is the premise of the ‘noisy neighbor’ problem. ‘Noisy neighbor’ describes the situation where an entity using shared resources takes more than was initially allocated for them, and thus at least one other entity does not have enough resources to do what they need.
While this problem has existed since the advent of shared hosting between a hosting provider and clients, it has especially come to the forefront as many companies are shifting to cloud-based, multi-tenanted infrastructure. Multi-tenancy simply refers to a single software instance supporting multiple user groups. 
If we imagine that the Abode project has multi-tenancy requirements, a major consideration will be: how do we make sure each tenant has the resources available to scale when they need to, without restricting or impacting other tenants? To do this, the solution will have to be elastic, configurable, and thoughtful. 

## Building a multi-tenanted solution
There are 4 discrete steps or considerations when initially building a multi-tenanted solution. We will go through each of these and discuss how ADT features/limitations may affect our choices.
1. **Onboarding**. For this step, we consider how tenants get introduced into your environment. How will they sign up? How will whatever resources they need be instantiated? Assuming each tenant wants their own TSI instance, at minimum we will be provisioning one of those in a way which is easily identifiable as belonging to that tenant. If we go with a fully siloed approach (more on this later), we may be provisioning an ADT instance per tenant.
1. **Authentication**. From the moment a user enters our environment, they should be associated with an identity that follows every action they take. An example of this would be a SAS token which is carried along every step of the architectural flow, so that entities can authenticate into whatever microservices we are using. With ADT, we can use Managed Service Identity (MSI), which works great alongside other services such as Azure Functions. 
1. **Tenant Isolation**. Related, but not equivalent to authentication, this step essentially asks the question: “how do we ensure tenants cannot reach across boundaries?” This can be done in two ways. The first is through siloed infrastructure, wherein resources are divided into physical partitions and each tenant gets its own environment to work in. A silo model eliminates the noisy neighbor problem, but it is more difficult to scale and typically much more expensive. A more modern multi-tenancy pattern uses the pool model, wherein resources and microservices are shared across tenants but, via runtime policies and role-based access control, you ensure tenants cannot reach across boundaries. Later, we discuss limitations with ADT that may affect which approach you choose.
1. **Application Logic**. Ideally, by this point you will not need to think about multi-tenancy much. If you have designed your architecture to support tenant isolation and authorization/authentication, from the developer perspective improving performance and accessing models in the ADT graph is just a matter of using the model identifier (and potentially tenant identifier) provided. 

## Considerations and limits for ADT

### **Relevant Service Limits**	
-	1 model graph per Azure Digital Twins instance
-	*10 Azure Digital Twin instances per region, per subscription (adjustable)
-	ADT is available in 10 regions
-	10,000 Models per Azure Digital Twins instance (adjustable)
-	200,000 Twins per Azure Digital Twins instance (adjustable)
-	2000 RPS to Digital Twin API per Twin (adjustable)			
-	500 RPS to Query API (adjustable)
-	4000 Query Units (CPU, IOPS, memory) /second (adjustable)
-	Event egress 100 RPS (adjustable)

### **If models are not standardized**
We see above that you can only have one model graph per Azure Digital Twins instance. Given this, if multiple tenants exist withing the same Azure Digital Twins Instance, you will need to consider how to handle model entropy; that is, how to delineate models of the same name that have different fields and belong to different tenants. 

If using the siloed approach described above, this is straightforward: no legal entropy will occur as each tenant will have their own graph, and thus any models with the same name may simply overwrite older models (the exact desired behavior of this, we would have to determine). 
With multiple tenants however, we cannot expect tenants to be aware of the naming other tenants are using. The simplest solution would be model standardization. If all tenants use the same models, then no entropy will occur. However, if for some reason project requirements specify that each tenant be allowed their own model definitions, to use a pool model you will have to find a workaround. One way could be associating tenantID with model names, but this could potentially introduce other issues especially if there is a character limit on model names (unknown, although maximum size of a single model is 1 MB). 

Another consideration is that there is a stated service limit of 10,000 models per instance. If using a pooled approach without model standardization, you would have to ensure that this limit is extremely flexible, or else come up with logic around what will happen as you approach your model limit (will tenants be moved into a new instance? Be told they need to reduce their number of models? Only be allowed a certain number of models each?)

### **If dealing with a huge number of tenants**
This is another place where the limits of “adjustable” will have to be more rigidly defined. As it stands, you are allowed 10 instances per region, per subscription. If you were using the siloed approach, since there are 10 available regions that would mean you would hit a maximum of 100 tenants before you had to create a new subscription. Many developers have the preference of only having to maintain a single subscription, which could make the siloed approach a nonstarter. 

### **Performance**
Many of the service limits exist per-Twin. Likely, if using the pooled approach, you would be having tenants sharing an Azure Digital Twin Instance, which supports 200k Twins, and not sharing Twins themselves. However, the Query API and Event Routes API do not explicitly state whether their limits are per-Twin or per-instance. This would be an important thing to clarify before deciding on a siloed or pooled approach, as those numbers per-instance could make Noisy Neighbors a very huge concern.

## Key Points
- The 'Noisy Neighbor' problem refers to when resources are shared, and one user takes up more of the resources than allocated. As a result, one or more other users are impacted. 
- When designing a multi-tenant architecture, the silo model can: lead to over-provisioned resources, make it difficult to scale, and spike cost. However, it has the advantage of physical isolation, which can increase tenant confidence that their data is safe as well as eliminated the noisy neighbor problem. 
- The pool model allows for cost-efficient elastic scaling, and by implementing logical separations of resources rather than physical, you can then still enforce RBAC as needed. However, it has the drawback of being more complex to design, as those logical separations require a careful and accurate approach, and you also have to find workarounds for the classic 'Noisy Neighbor' problem.
- Model standardization is a great way to simplify the pooled approach when working with ADT. Otherwise, you will have to brainstorm solutions to avoid model entropy.

## Learning Exercises

### Authenticate a client to an Azure Digital Twin instance
[This tutorial]( https://docs.microsoft.com/en-us/azure/digital-twins/how-to-authenticate-client) guides you through how to authenticate a client against ADT. Focus on MSI here.

### Grant a user access to resources using the Portal or PowerShell
RBAC allows you to grant users access to specifically scoped resources, from subscriptions to a particular container. [Follow this tutorial to get familiarized with RBAC](https://docs.microsoft.com/en-us/azure/role-based-access-control/tutorial-role-assignments-user-powershell). Grant a user Reader permissions—we will test this in Experiments below.

### Use RBAC to secure access to blob data 
Follow [this tutorial](https://docs.microsoft.com/en-us/azure/storage/common/storage-auth-aad-rbac-portal) for guidance on how you would use RBAC with storage blobs.

## Experiments
- After completing the first exercise above, check that you cannot access your secured resources without the correct identity.
- After completing the second exercise above, check that your user with Reader permissions cannot modify or delete the resource. 

## Things to Consider 
- In what cases would you use the silo model or the pool model when working with ADT? What are some things that need to be true for one or the other to work?
- If using a pool model and tenants are allowed to each have their own model definitions, how would you associate certain models with certain tenants?
- What are some examples of logical partitions that would allow you to use the silo model without compromising data that should be isolated?