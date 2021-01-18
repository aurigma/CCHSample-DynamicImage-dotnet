# Customer's Canvas / Dynamic Image Sample App

## What is it? 

This is a sample project which demonstrates how to use Dynamic Image and Customer's Canvas to create image personalization based on Photoshop files. In this application, you are using [Customer's Canvas backend](https://customerscanvas.com/docfx/dev/intro.html) to manage files and [Dynamic Image API](https://customerscanvas.com/support/dynamic-image/readme.md) to personalize images.

The application is written with .NET Core as a backend and plain HTML/JavaScript as a frontend. The only exception is a HTML form which is used to display a list of fields and edit the values. It is built with [Stencil toolchain](https://stenciljs.com/). It is pre-build and integrated to a project. If you need the source code for the form builder itself, please [contact us](https://customerscanvas.com/company/contact).

## How it works? 

To run this app, you need to have a Customer's Canvas system instance (either an account at a cloud version or an on-premises installation). In the Assets\Images section of your account, you need to create a folder and upload some PSD files.

The app connects to Customer's Canvas and receives a list of all PSD files from this folder. For brevity, the folder is hardcoded, but the API allows listing folders and request files from other folders, so you may create more complicated PSD file browser.

All these items are displayed on a page. When a user chooses a PSD file, it opens an image personalization page. Here, the Dynamic Image API is used for two things: 

- Get a list of layers in a selected PSD file. This information is used to build a personalization form.
- Generate a personalized preview based on the form input. 

### Configuration

To be able to run this sample app, you need to fill in the **appsettings.config**:

```
{
  // ...
  "CustomersCanvas": {
    "ApiUrl": "",
    "ClientId": "",
    "ClientSecret": "",
    "DynamicImageVersion": "",
    "IdentityProviderUrl": "",
    "TenantId": ""
  }
}
```

If you have an instance at our cloud version, use the following values: 

- `ApiUrl` - https://api.customerscanvashub.com
- `IdentityProviderUrl` - https://customerscanvashub.com
- `ClientId` and `ClientSecret` - go to BackOffice, register your client (Settings -> External Apps), and use the appropriate values these.
- `TenantId` and `DynamicImageVersion` - contact our support team for this information.

> **NOTE:** For simplicity, you may just put these values to the **appsettings.config**. However, for the real-life applications it is highly recommended using [user secrets functionality as per Microsoft docs](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows).

If you are not interesting in using BackOffice (and, therefore, our identity server), you may connect it to your OAuth2 identity provider, as soon as it returns tokens containing the claims used by Customer's Canvas. Contact us for more information about it.

### Customer's Canvas API

In this application, we are using an official Customer's Canvas .NET API Client library. See the [.NET tutorial in Customer's Canvas docs](https://customerscanvas.com/docfx/dev/tutorials/cs-api-client.html?tabs=dotnetcore) for more details how to work with this library and configure Dependency Injection to instantiate it efficiently.

If you are interested to port it to another platform, you may want to generate an API client for your platform from Open API 3 specs or use the REST API directly. In this case, refer [Customer's Canvas Back-End API reference](https://customerscanvas.com/docfx/dev/api-reference.html).

The API client is used in the controllers, so you may take a look at the files in the **Controllers** folder for the usage examples.

### Using Dynamic Image

The most interesting part of the project is the **Views/Images/Edit.cshtml** file. It contains a code which works with Dynamic Image API. In this sample, REST API is used directly through `fetch`. 

First, it sends the `POST ~/api/information/template` request to get all layers for a specified PSD file. The output of this request is sent to the `<di-form>` element. In a real-life application, you will create your own implementation of this form, so it is taken out of this project for brevity. 

> **NOTE:** If all your PSD files have the same structure and you are following a consistent naming convention, this part is optional, as you may hardcode the list of fields.  

When the user modifies the form, a `POST ~/api/rendering/preview` request is sent. Here, you are sending a JSON object which contains a dictionary where the key is a layer name and a value is a so-called _command_ which says what you want to do with this layer. For example, change the shape color or replace the image content by a specified URL. 

This process is described in [Dynamic Image docs](https://customerscanvas.com/support/dynamic-image/rendering.md), feel free to refer it if you want to find out more information how to work with this API.

## PSD files

For more information about how to create PSD files compatible with Dynamic Image, refer the [PSD Files article](https://customerscanvas.com/support/dynamic-image/psd-files.md) of the Dynamic Image docs. Here you can understand which features of Photoshop are available and what you should not do. 

You may learn more about Dynamic Image API in the [official documentation](https://customerscanvas.com/support/dynamic-image/readme.md). 

---
If you have any questions or problems regarding this API, feel free to [contact our support team](https://customerscanvas.com/company/contact).
