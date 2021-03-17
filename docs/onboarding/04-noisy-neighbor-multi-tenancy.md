# Noisy Neighbors & Multi-tenancy

- [Isolation in Azure](https://docs.microsoft.com/en-us/azure/security/fundamentals/isolation-choices) - ~15 min, focus on reading Tenant Isolation, Azure Tenancy, Azure role-based access control (Azure RBAC), and Storage Isolation 
- [Custom roles in RBAC](https://docs.microsoft.com/en-us/azure/role-based-access-control/custom-roles) - ~20 min including following steps

## Overview
Imagine you have a neighbor who plays the Gilligan’s Island theme song 24/7. You might have trouble concentrating, or even doing anything at all. This is the premise of the ‘noisy neighbor’ problem. ‘Noisy neighbor’ describes the situation where an entity using shared resources takes more than was initially allocated for them, and thus at least one other entity does not have enough resources to do what they need.
While this problem has existed since the advent of shared hosting between a hosting provider and clients, it has especially come to the forefront as many companies are shifting to cloud-based, multi-tenanted infrastructure. Multi-tenancy simply refers to a single software instance supporting multiple user groups. 
If we imagine that the Abode project has multi-tenancy requirements, a major consideration will be: how do we make sure each tenant has the resources available to scale when they need to, without restricting or impacting other tenants? To do this, the solution will have to be elastic, configurable, and thoughtful. 

## Building a multi-tenanted solution
There are 4 discrete steps or considerations when initially building a multi-tenanted solution. 
1. **Onboarding**. For this step, we consider how tenants get introduced into your environment. How will they sign up? How will whatever resources they need be instantiated?
1. **Authentication**. From the moment a user enters our environment, they should be associated with an identity that follows every action they take. An example of this would be a SAS token which is carried along every step of the architectural flow, so that entities can authenticate into whatever microservices we are using.  
1. **Tenant Isolation**. Related, but not equivalent to authentication, this step essentially asks the question: “how do we ensure tenants cannot reach across boundaries?” This can be done in two ways. The first is through siloed infrastructure, wherein resources are divided into physical partitions and each tenant gets its own environment to work in. A silo model eliminates the noisy neighbor problem, but it is more difficult to scale and typically much more expensive. A more modern multi-tenancy pattern uses the pool model, wherein resources and microservices are shared across tenants but, via runtime policies and role-based access control, you ensure tenants cannot reach across boundaries. 
1. **Application**. This is the central part of your architecture. Ideally, by this point you will not need to think about multi-tenancy much. If you have designed your architecture to support tenant isolation and authorization/authentication, from the developer perspective it’s just a matter of figuring out how many instances of your application and other resources you need to provision.

## Key Points
- The 'Noisy Neighbor' problem refers to when resources are shared, and one user takes up more of the resources than allocated. As a result, one or more other users are impacted. 
- When designing a multi-tenant architecture, the silo model can: lead to over-provisioned resources, make it difficult to scale, and spike cost. However, it has the advantage of physical isolation, which can increase tenant confidence that their data is safe as well as eliminated the noisy neighbor problem. 
- The pool model allows for cost-efficient elastic scaling, and by implementing logical separations of resources rather than physical, you can then still enforce RBAC as needed. However, it has the drawback of being more complex to design, as those logical separations require a careful and accurate approach, and you also have to find workarounds for the classic 'Noisy Neighbor' problem.

## Learning Exercises

### Grant a user access to resources using the Portal or PowerShell
RBAC allows you to grant users access to specifically scoped resources, from subscriptions to a particular container. [Follow this tutorial to get familiarized with RBAC](https://docs.microsoft.com/en-us/azure/role-based-access-control/tutorial-role-assignments-user-powershell).

### Use RBAC to secure access to blob data 
Follow [this tutorial](https://docs.microsoft.com/en-us/azure/storage/common/storage-auth-aad-rbac-portal) for guidance on how you would use RBAC with storage blobs.

## Experiments
Check that you can not access your secured resources without the correct identity. 

## Things to Consider 
- In what cases would you use the silo model or the pool model?
- What are some examples of logical partitions that would allow you to use the silo model without compromising data that should be isolated?