# Onboarding

The following outlines resources that can be used for getting up to speed with Azure Digital Twins

## Prerequisites

- [Azure Subscription](https://azure.microsoft.com/en-gb/free/?WT.mc_id=A261C142F)
  - A role that has [permissions](https://docs.microsoft.com/en-gb/azure/digital-twins/how-to-set-up-instance-portal#prerequisites-permission-requirements) to create and manage resources, as well as user access to resources is required.
- [Azure CLI](https://docs.microsoft.com/en-gb/cli/azure/install-azure-cli)
  - [Set up your session](https://docs.microsoft.com/en-us/azure/digital-twins/tutorial-end-to-end#set-up-cloud-shell-session) and register your Azure Subscription with the Azure Digital Twins namespace
- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/)
  - Ensure your installation of Visual Studio 2019 includes the [Azure Development workload](https://docs.microsoft.com/en-us/dotnet/azure/configure-visual-studio). This workload enables the application to publish Azure Function Apps.
- [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://docs.docker.com/get-docker/)

## Chapters

> **NOTE:** These documents all focus around features in C#, so bear that in mind if working on a Java project.

1. [What is Azure Digital Twins?](01-adt-overview.md)
2. [Digital Twin Definition Language](02-digital-twin-definition-language.md)
3. [Digital Twin SDK & API](03-sdks-and-apis.md)
4. [Noisy Neighbors & Multi-tenancy](04-noisy-neighbor-multi-tenancy.md)
5. [Deploy and Run E2E Sample](05-e2e-sample.md)
6. [Time Series Insights](06-time-series-insights.md)
7. [Query Performance](07-query-performance.md)
