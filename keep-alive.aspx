<%@ Page Language="C#" AutoEventWireup="true" CodeFile="keep-alive.aspx.cs" Inherits="keep_alive" %>

<%--    
        This page is embedded in the glogal masterpage in a 0x0 iframe.
        A refresh header is set for 1 minute before the end of the current session.
        The refresh enables the session to extend until the user logs out,
        instead of the forced session-timeout.

--%>