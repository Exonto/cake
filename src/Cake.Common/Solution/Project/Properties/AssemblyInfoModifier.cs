// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Common.Solution.Project.Properties
{
    /// <summary>
    /// The assembly info modifier.
    /// </summary>
    public sealed class AssemblyInfoModifier
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly ICakeLog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyInfoModifier"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        public AssemblyInfoModifier(IFileSystem fileSystem, ICakeEnvironment environment)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            _fileSystem = fileSystem;
            _environment = environment;
        }

        /// <summary>
        /// Modifies an assemly info file.
        /// </summary>
        /// <param name="assemblyInfoPath"></param>
        public void Modify(FilePath assemblyInfoPath)
        {
            if (assemblyInfoPath == null)
            {
                throw new ArgumentNullException(nameof(assemblyInfoPath));
            }

            if (assemblyInfoPath.IsRelative)
            {
                assemblyInfoPath = assemblyInfoPath.MakeAbsolute(_environment);
            }

            var file = _fileSystem.GetFile(assemblyInfoPath);

            if (!file.Exists)
            {
                const string format = "Assembly info file '{0}' does not exist.";
                var message = string.Format(CultureInfo.InvariantCulture, format, assemblyInfoPath.FullPath);
                throw new CakeException(message);
            }
        }
    }
}
