# WebApiAsWorkerService
An example of deploying a .NET Core 6 Web Api as a windows service

To install as win service run in Command Prompt (as Admin):
sc create WindowsServiceWebApiDemo binpath= D:\dmitrii_dikhtiar\examples\WorkerService\publish\WebApi.exe start= auto DisplayName= "WindowsServiceWebApiDemo"

To delete service run in Command Prompt (as Admin):
sc delete WindowsServiceWebApiDemo
