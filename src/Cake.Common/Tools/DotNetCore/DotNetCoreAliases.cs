﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.Clean;
using Cake.Common.Tools.DotNetCore.Execute;
using Cake.Common.Tools.DotNetCore.NuGet.Delete;
using Cake.Common.Tools.DotNetCore.NuGet.Push;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Common.Tools.DotNetCore.Run;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.Common.Tools.DotNetCore
{
    /// <summary>
    /// <para>Contains functionality related to <see href="https://github.com/dotnet/cli">.NET Core CLI</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, the .Net Core CLI tools will need to be installed on the machine where
    /// the Cake script is being executed.  See this <see href="https://www.microsoft.com/net/core">page</see> for information
    /// on how to install.
    /// </para>
    /// </summary>
    [CakeAliasCategory("DotNetCore")]
    public static class DotNetCoreAliases
    {
        /// <summary>
        /// Execute an assembly.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreExecute("./bin/Debug/app.dll");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Execute")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Execute")]
        public static void DotNetCoreExecute(this ICakeContext context, FilePath assemblyPath)
        {
            context.DotNetCoreExecute(assemblyPath, null);
        }

        /// <summary>
        /// Execute an assembly with arguments in the specific path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreExecute("./bin/Debug/app.dll", "--arg");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Execute")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Execute")]
        public static void DotNetCoreExecute(this ICakeContext context, FilePath assemblyPath, ProcessArgumentBuilder arguments)
        {
            context.DotNetCoreExecute(assemblyPath, arguments, null);
        }

        /// <summary>
        /// Execute an assembly with arguments in the specific path with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreExecuteSettings
        ///     {
        ///         FrameworkVersion = "1.0.3"
        ///     };
        ///
        ///     DotNetCoreExecute("./bin/Debug/app.dll", "--arg", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Execute")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Execute")]
        public static void DotNetCoreExecute(this ICakeContext context, FilePath assemblyPath, ProcessArgumentBuilder arguments, DotNetCoreExecuteSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (assemblyPath == null)
            {
                throw new ArgumentNullException(nameof(assemblyPath));
            }

            if (settings == null)
            {
                settings = new DotNetCoreExecuteSettings();
            }

            var executor = new DotNetCoreExecutor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            executor.Execute(assemblyPath, arguments, settings);
        }

        /// <summary>
        /// Restore all NuGet Packages.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRestore();
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Restore")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Restore")]
        public static void DotNetCoreRestore(this ICakeContext context)
        {
            context.DotNetCoreRestore(null, null);
        }

        /// <summary>
        /// Restore all NuGet Packages in the specified path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="root">List of projects and project folders to restore. Each value can be: a path to a project.json or global.json file, or a folder to recursively search for project.json files.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRestore("./src/*");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Restore")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Restore")]
        public static void DotNetCoreRestore(this ICakeContext context, string root)
        {
            context.DotNetCoreRestore(root, null);
        }

        /// <summary>
        /// Restore all NuGet Packages with the settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreRestoreSettings
        ///     {
        ///         Sources = new[] {"https://www.example.com/nugetfeed", "https://www.example.com/nugetfeed2"},
        ///         FallbackSources = new[] {"https://www.example.com/fallbacknugetfeed"},
        ///         PackagesDirectory = "./packages",
        ///         Verbosity = Information,
        ///         DisableParallel = true,
        ///         InferRuntimes = new[] {"runtime1", "runtime2"}
        ///     };
        ///
        ///     DotNetCoreRestore(settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Restore")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Restore")]
        public static void DotNetCoreRestore(this ICakeContext context, DotNetCoreRestoreSettings settings)
        {
            context.DotNetCoreRestore(null, settings);
        }

        /// <summary>
        /// Restore all NuGet Packages in the specified path with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="root">List of projects and project folders to restore. Each value can be: a path to a project.json or global.json file, or a folder to recursively search for project.json files.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreRestoreSettings
        ///     {
        ///         Sources = new[] {"https://www.example.com/nugetfeed", "https://www.example.com/nugetfeed2"},
        ///         FallbackSources = new[] {"https://www.example.com/fallbacknugetfeed"},
        ///         PackagesDirectory = "./packages",
        ///         Verbosity = Information,
        ///         DisableParallel = true,
        ///         InferRuntimes = new[] {"runtime1", "runtime2"}
        ///     };
        ///
        ///     DotNetCoreRestore("./src/*", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Restore")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Restore")]
        public static void DotNetCoreRestore(this ICakeContext context, string root, DotNetCoreRestoreSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreRestoreSettings();
            }

            var restorer = new DotNetCoreRestorer(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log);
            restorer.Restore(root, settings);
        }

        /// <summary>
        /// Build all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreBuild("./src/*");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Build")]
        public static void DotNetCoreBuild(this ICakeContext context, string project)
        {
            context.DotNetCoreBuild(project, null);
        }

        /// <summary>
        /// Build all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreBuildSettings
        ///     {
        ///         Framework = "netcoreapp1.0",
        ///         Configuration = "Debug",
        ///         OutputDirectory = "./artifacts/"
        ///     };
        ///
        ///     DotNetCoreBuild("./src/*", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Build")]
        public static void DotNetCoreBuild(this ICakeContext context, string project, DotNetCoreBuildSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreBuildSettings();
            }

            var builder = new DotNetCoreBuilder(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            builder.Build(project, settings);
        }

        /// <summary>
        /// Package all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <example>
        /// <code>
        ///     DotNetCorePack("./src/*");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Pack")]
        public static void DotNetCorePack(this ICakeContext context, string project)
        {
            context.DotNetCorePack(project, null);
        }

        /// <summary>
        /// Package all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCorePackSettings
        ///     {
        ///         Configuration = "Release",
        ///         OutputDirectory = "./artifacts/"
        ///     };
        ///
        ///     DotNetCorePack("./src/*", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pack")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Pack")]
        public static void DotNetCorePack(this ICakeContext context, string project, DotNetCorePackSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCorePackSettings();
            }

            var packer = new DotNetCorePacker(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            packer.Pack(project, settings);
        }

        /// <summary>
        /// Run all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRun();
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Run")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Run")]
        public static void DotNetCoreRun(this ICakeContext context)
        {
            context.DotNetCoreRun(null, null, null);
        }

        /// <summary>
        /// Run project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRun("./src/Project");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Run")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Run")]
        public static void DotNetCoreRun(this ICakeContext context, string project)
        {
            context.DotNetCoreRun(project, null, null);
        }

        /// <summary>
        /// Run project with path and arguments.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRun("./src/Project", "--args");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Run")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Run")]
        public static void DotNetCoreRun(this ICakeContext context, string project, ProcessArgumentBuilder arguments)
        {
            context.DotNetCoreRun(project, arguments, null);
        }

        /// <summary>
        /// Run project with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreRunSettings
        ///     {
        ///         Framework = "netcoreapp1.0",
        ///         Configuration = "Release"
        ///     };
        ///
        ///     DotNetCoreRun("./src/Project", "--args", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Run")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Run")]
        public static void DotNetCoreRun(this ICakeContext context, string project, ProcessArgumentBuilder arguments, DotNetCoreRunSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreRunSettings();
            }

            var runner = new DotNetCoreRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(project, arguments, settings);
        }

        /// <summary>
        /// Publish all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <example>
        /// <code>
        ///     DotNetCorePublish("./src/*");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Publish")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Publish")]
        public static void DotNetCorePublish(this ICakeContext context, string project)
        {
            context.DotNetCorePublish(project, null);
        }

        /// <summary>
        /// Publish all projects.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCorePublishSettings
        ///     {
        ///         Framework = "netcoreapp1.0",
        ///         Configuration = "Release",
        ///         OutputDirectory = "./artifacts/"
        ///     };
        ///
        ///     DotNetCorePublish("./src/*", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Publish")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Publish")]
        public static void DotNetCorePublish(this ICakeContext context, string project, DotNetCorePublishSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCorePublishSettings();
            }

            var publisher = new DotNetCorePublisher(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            publisher.Publish(project, settings);
        }

        /// <summary>
        /// Test project.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreTest();
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreTest(this ICakeContext context)
        {
            context.DotNetCoreTest(null, null);
        }

        /// <summary>
        /// Test project with path.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreTest("./src/Project");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreTest(this ICakeContext context, string project)
        {
            context.DotNetCoreTest(project, null);
        }

        /// <summary>
        /// Test project with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreTestSettings
        ///     {
        ///         Configuration = "Release"
        ///     };
        ///
        ///     DotNetCoreTest("./test/Project.Tests", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Test")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Test")]
        public static void DotNetCoreTest(this ICakeContext context, string project, DotNetCoreTestSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreTestSettings();
            }

            var tester = new DotNetCoreTester(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tester.Test(project, settings);
        }

        /// <summary>
        /// Cleans a project's output.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The project's path.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreClean("./src/project");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Clean")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Clean")]
        public static void DotNetCoreClean(this ICakeContext context, string project)
        {
            context.DotNetCoreClean(project, null);
        }

        /// <summary>
        /// Cleans a project's output.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="project">The projects path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreCleanSettings
        ///     {
        ///         Framework = "netcoreapp1.0",
        ///         Configuration = "Debug",
        ///         OutputDirectory = "./artifacts/"
        ///     };
        ///
        ///     DotNetCoreClean("./src/project", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Clean")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.Clean")]
        public static void DotNetCoreClean(this ICakeContext context, string project, DotNetCoreCleanSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreCleanSettings();
            }

            var cleaner = new DotNetCoreCleaner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            cleaner.Clean(project, settings);
        }

        /// <summary>
        /// Delete a NuGet Package from a server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreNuGetDelete();
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context)
        {
            context.DotNetCoreNuGetDelete(null, null, null);
        }

        /// <summary>
        /// Deletes a package from the NuGet.org.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to delete.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreNuGetDelete("Microsoft.AspNetCore.Mvc");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context, string packageName)
        {
            context.DotNetCoreNuGetDelete(packageName, null, null);
        }

        /// <summary>
        /// Deletes a specific version of a package from the NuGet.org.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to delete.</param>
        /// <param name="packageVersion">Version of package to delete.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreRestore("Microsoft.AspNetCore.Mvc", "1.0");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context, string packageName, string packageVersion)
        {
            context.DotNetCoreNuGetDelete(packageName, packageVersion, null);
        }

        /// <summary>
        /// Deletes a package from a server
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to delete.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreNuGetDeleteSettings
        ///     {
        ///         Source = "https://www.example.com/nugetfeed",
        ///         NonInteractive = true
        ///     };
        ///
        ///     DotNetCoreNuGetDelete("Microsoft.AspNetCore.Mvc", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context, string packageName, DotNetCoreNuGetDeleteSettings settings)
        {
            context.DotNetCoreNuGetDelete(packageName, null, settings);
        }

        /// <summary>
        /// Deletes a package from a server using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new DotNetCoreNuGetDeleteSettings
        ///     {
        ///         Source = "https://www.example.com/nugetfeed",
        ///         NonInteractive = true
        ///     };
        ///
        ///     DotNetCoreNuGetDelete(settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context, DotNetCoreNuGetDeleteSettings settings)
        {
            context.DotNetCoreNuGetDelete(null, null, settings);
        }

        /// <summary>
        /// Deletes a package from a server using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to delete.</param>
        /// <param name="packageVersion">Version of package to delete.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///  <code>
        ///     var settings = new DotNetCoreNuGetDeleteSettings
        ///     {
        ///         Source = "https://www.example.com/nugetfeed",
        ///         NonInteractive = true
        ///     };
        ///
        ///     DotNetCoreNuGetDelete("Microsoft.AspNetCore.Mvc", "1.0", settings);
        ///  </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Delete")]
        public static void DotNetCoreNuGetDelete(this ICakeContext context, string packageName, string packageVersion, DotNetCoreNuGetDeleteSettings settings)
        {
             if (context == null)
             {
                 throw new ArgumentNullException(nameof(context));
             }

             if (settings == null)
             {
                 settings = new DotNetCoreNuGetDeleteSettings();
             }

             var restorer = new DotNetCoreNuGetDeleter(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
             restorer.Delete(packageName, packageVersion, settings);
        }

        /// <summary>
        /// Pushes one or more packages to a server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to push.</param>
        /// <example>
        /// <code>
        ///     DotNetCoreNuGetPush("*.nupkg");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Push")]
        public static void DotNetCoreNuGetPush(this ICakeContext context, string packageName)
        {
            context.DotNetCoreNuGetPush(packageName, null);
        }

        /// <summary>
        /// Pushes one or more packages to a server using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="packageName">Name of package to push.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///  <code>
        ///     var settings = new DotNetCoreNuGetPushSettings
        ///     {
        ///         Source = "https://www.example.com/nugetfeed",
        ///         ApiKey = "4003d786-cc37-4004-bfdf-c4f3e8ef9b3a"
        ///     };
        ///
        ///     DotNetCoreNuGetPush("foo*.nupkg", settings);
        ///  </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("NuGet")]
        [CakeNamespaceImport("Cake.Common.Tools.DotNetCore.NuGet.Push")]
        public static void DotNetCoreNuGetPush(this ICakeContext context, string packageName, DotNetCoreNuGetPushSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (settings == null)
            {
                settings = new DotNetCoreNuGetPushSettings();
            }

            var restorer = new DotNetCoreNuGetPusher(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            restorer.Push(packageName, settings);
        }
    }
}