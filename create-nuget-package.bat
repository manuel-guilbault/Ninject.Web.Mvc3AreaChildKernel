src\.nuget\nuget.exe pack src\Ninject.Web.MvcAreaChildKernel\Ninject.Web.MvcAreaChildKernel.csproj -IncludeReferencedProjects -Build -Prop Configuration=Release -OutputDirectory dist
src\.nuget\nuget.exe pack src\Ninject.Web.MvcAreaChildKernel\Ninject.Web.MvcAreaChildKernel.csproj -Symbols -Build -Prop Configuration=Release -OutputDirectory dist
