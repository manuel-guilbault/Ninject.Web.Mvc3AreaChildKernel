Ninject.Web.MvcAreaChildKernel
==============================

Use child kernels with MVC 3+ areas.

Why
---

In big MVC applications, it can get confusing to have all services registered in the same kernel. Even when using Ninject modules, all services are available to the whole application, which can lead to complicated conditional bindings, turning the composition root into a huge mess.

By using child kernels for MVC areas, you can isolate the services available to a single area into a child kernel. And because child kernels will redirect resolution requests they cannot fullfil to their parent, you can still have all global services registered at the application level and visible to all areas.

How
---

Assuming you already have a MVC project named MyMvcApp with 

* a reference on the [Ninject.Web.MvcAreaChildKernel](https://github.com/manuel-guilbault/Ninject.Web.MvcAreaChildKernel) extension and its dependencies
  * [Ninject.Web.Mvc](https://github.com/ninject/ninject.web.mvc)
  * [Ninject.Extensions.ChildKernel](https://github.com/ninject/ninject.extensions.childkernel)
* an area named MyArea

Go ahead and create a child kernel in your area:

```csharp
namespace MyMvcApp.Areas.MyArea
{
    public class MyAreaKernel : ChildKernel
    {
        public MyAreaKernel(IResolutionRoot parent)
            : base(parent)
        {
            // Declare the bindings available for the area...
        }
    }
}
```

Then you simply link your area to the child kernel:
```csharp
namespace MyMvcApp.Areas.MyArea
{
    public class MyAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            return "MyArea";
        }
        
        public override void RegisterArea(AreaRegistrationContext context)
        {
            // Map area routes...
            
            // Add this line to your MyAreaAreaRegistration class: 
            context.UseKernel(parent => new MyAreaKernel(parent));
        }
    }
}
```

The call to the ```context.UseKernel``` extension method will link the child kernel factory lambda expression to the area being registered, so all controllers, action filters and view pages used in this area will be created (and scoped) using this child kernel.

The ```context.UseKernel``` extension method will bind the child kernel to the global kernel, so when the application shuts down and disposes the global kernel, all child kernels linked to MVC areas will also be disposed.

Try it out!
-----------

You can download the source code or fork the repository to run the MvcSample application included in the solution.
