# dotnet-secure-file-upload
Demonstrates how to implement a secure file upload form/api and adding validation/virus protection mechanisms

## Overview
This repository demonstrates how to integrate the following mechanisms to secure file uploads:

- Virus Scan, using (Cloudmersive Virus Scan API)[https://cloudmersive.com/virus-api]
- Parse and Validation, using (TinyCsvParser)[http://bytefish.github.io/TinyCsvParser/sections/quickstart.html]

It also demonstrates the difference of using local storage vs (Azure Blob Storage)[https://docs.microsoft.com/en-us/azure/storage/blobs/].

Last but not least, demonstrates how to turn a synchronous and blocking process in a .Net Framework application to an asynchrounous one using (Azure Functions)[https://docs.microsoft.com/en-us/azure/azure-functions/] and (Azure Service Bus Messaging)[https://docs.microsoft.com/en-us/azure/service-bus-messaging/] taking into account the security measures described above.

## Usage
Included is a GitHub Workflow that builds and deploys all resources needed in Azure, to make this work you will need the following secrets:

- **AZURE_CREDENTIALS**, SDK credentials to create a resource group and deploy an ARM template to it.
- **AZURE_SUBSCRIPTION_ID**, the ID of the Azure Subscription
- **CLOUDMERSIVEVIRUSSCANAPIKEY**, a valid Key for the Cloudmersive Virus Scan API
