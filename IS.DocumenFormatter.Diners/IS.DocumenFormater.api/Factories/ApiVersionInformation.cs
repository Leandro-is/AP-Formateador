using System.Reflection;
using System;

namespace IS.DocumenFormater.api.Factories
{
  public static class ApiVersionInformation
  {
    public static readonly String CurrentVersionApi = GetCurrentVersionSemver();

    private static string GetCurrentVersionSemver()
    {
      // Obtener la versión informacional completa
      String fullVersion = typeof(Constants).Assembly
          .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

      // Dividir la cadena en dos partes: versión y commit
      string[] versionParts = fullVersion.Split('+');

      // Asegurarse de que hay un commit hash
      string semVer = versionParts[0]; // Esta es la versión SemVer, por ejemplo, 1.1.2
      string commitHash = versionParts.Length > 1 ? versionParts[1].Substring(0, 7) : ""; // Tomar los primeros 7 caracteres del commit

      // Combinar la versión SemVer con los primeros 7 caracteres del commit
      string versionWithCommit = $"{semVer}+{commitHash}";
      return versionWithCommit;
    }
  }
}
