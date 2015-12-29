<%@ Page CodeBehind="Demo.aspx.cs" Inherits="BaseProduct.Actions.Demo" Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Title</title>
    </head>
    <body>
        <form id="HtmlForm" runat="server">
            <div>
                <h1>Dogs</h1>
                <ul>
                    <% foreach(var name in Model.Names) { %>
                    <li><%= name %></li>
                    <% } %>
                </ul>
            </div>
        </form>
    </body>
</html>
