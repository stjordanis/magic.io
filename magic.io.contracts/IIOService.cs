﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using magic.io.web.model;

namespace magic.io.contracts
{
    public interface IIOService
    {
        IEnumerable<Folder> GetFolders(string path);

        IEnumerable<File> GetFiles(string path);

        FileResult GetFile(string path);
    }
}
