<%@ WebHandler Language="C#" Class="SecurePdf" %>

using DefaultClass;
using EntitiesClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Globalization;

public class SecurePdf : IHttpHandler
{

    Global gb = new Global();
    GlobalEntities ge = new GlobalEntities();
    public void ProcessRequest(HttpContext context)
    {
        string encrypted = context.Request.QueryString["f"];
        if (string.IsNullOrEmpty(encrypted))
            return;

        string virtualPath = gb.Decrypt(encrypted);

        if (!virtualPath.StartsWith("~/Files/"))
        {
            context.Response.StatusCode = 403;
            return;
        }

        string physicalPath = context.Server.MapPath(virtualPath);

        if (!File.Exists(physicalPath))
        {
            context.Response.StatusCode = 404;
            return;
        }

        context.Response.ContentType = "application/pdf";
        context.Response.TransmitFile(physicalPath);
        context.Response.End();
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
