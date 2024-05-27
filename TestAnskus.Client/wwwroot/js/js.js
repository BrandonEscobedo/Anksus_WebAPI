function invokeCSharpFunction() {
        // Llama al método C# utilizando el servicio IJSRuntime de Blazor
        DotNet.invokeMethodAsync('AssemblyName', 'Namespace.ComponenteBlazor.MyCSharpFunction');
}

window.LlamarBlazor = function () {
    var result = window.DotNet.invokeMethodAsync("TestAnskus.Client", "CrearNombre", "brandon");
    alert(result);
}