﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Ninject;
using magic.io.contracts;

namespace magic.io.services.utilities
{
    public class Utilities
    {
        readonly IKernel _kernel;

        public Utilities(IConfiguration configuration, IKernel kernel)
        {
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            Root = (configuration["io:root-folder"] ?? "~/files")
                .Replace("~", Directory.GetCurrentDirectory())
                .TrimEnd('/') + "/";
        }

        public string Root { get; private set; }

        public string GetRelativePath(string absolute)
        {
            if (absolute.IndexOf(Root) != 0)
                throw new ArgumentOutOfRangeException($"'{absolute}' is not an absolute path and hence cannot be relativized");

            return "/" + absolute.Substring(Root.Length);
        }

        public string GetFullPath(string path = null, bool isFolder = false)
        {
            if (isFolder)
                return (Root + path?.Trim('/')).TrimEnd('/') + "/";
            return Root + path?.TrimStart('/');
        }

        public string GetMimeType(string filename)
        {
            return MimeTypes.GetMimeType(filename);
        }

        public bool HasAccess(
            string path,
            string username,
            string[] roles,
            AccessType type)
        {
            var authorize = _kernel.TryGet<IAuthorize>();
            if (authorize == null)
                return true;
            return authorize.Authorize(path, username, roles, type);
        }
    }
}
