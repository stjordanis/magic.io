﻿using System.Security.Claims;
using System.Linq;
using System;
/*
 * Magic, Copyright(c) Thomas Hansen 2019 - thomas@gaiasoul.com
 * Licensed as Affero GPL unless an explicitly proprietary license has been obtained.
 */

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using magic.io.contracts;
using magic.io.web.model;

namespace magic.io.web.controller
{
    /// <summary>
    /// IO controller for manipulating files and folders on your server
    /// </summary>
    [Route("api/io")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class IOController : ControllerBase
    {
        readonly IIOService _service;

        /// <summary>
        /// Creates a new instance of your IO controller
        /// </summary>
        /// <param name="service">Service containing business logic</param>
        public IOController(IIOService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Returns all folders inside of the specified folder
        /// </summary>
        /// <param name="folder">Folder to return folders from within</param>
        /// <returns>List of all folders inside of the specified folder</returns>
        [HttpGet]
        [Route("folders")]
        public ActionResult<IEnumerable<Folder>> GetFolders(string folder)
        {
            return Ok(_service.GetFolders(
                folder,
                User.Identity.Name,
                User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray()));
        }

        /// <summary>
        /// Returns all files inside of the specified folder
        /// </summary>
        /// <param name="folder">Folder to return files from within</param>
        /// <returns>List of all files inside of the specified folder</returns>
        [HttpGet]
        [Route("files")]
        public ActionResult<IEnumerable<File>> GetFiles(string folder)
        {
            return Ok(_service.GetFiles(
                folder,
                User.Identity.Name,
                User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray()));
        }

        /// <summary>
        /// Returns the specified file to caller
        /// </summary>
        /// <param name="file">File to return</param>
        /// <returns>The specified file</returns>
        [HttpGet]
        [Route("download")]
        public FileResult Download(string file)
        {
            return _service.GetFile(
                file,
                User.Identity.Name,
                User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray());
        }
    }
}
