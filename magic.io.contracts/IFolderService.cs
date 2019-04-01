﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System.Collections.Generic;

namespace magic.io.contracts
{
    public interface IFolderService
    {
        IEnumerable<string> GetFolders(string path, string username, string[] roles);

        IEnumerable<string> GetFiles(string path, string username, string[] roles);

        void Delete(string path, string username, string[] roles);

        void Create(string path, string username, string[] roles);
    }
}
