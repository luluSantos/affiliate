<%@ Page Title="Referral" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Referral.aspx.vb" Inherits="affiliate.Referral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Ads</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Default.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Referral.aspx">Ads</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="content_wrapper clearfix">
        <div class="sections_group">
            <div class="entry-content">
                <div class="section section-border-top flv_sections_21" id="iconbox">
                    <div class="section_wrapper clearfix">
                        <div class="items_group clearfix">
                            <div class="column one">
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
                            </div>

                            <div class="column one column_tabs">
                                <div class="jq-tabs tabs_wrapper tabs_horizontal">
                                    <ul>
                                        <li>
                                            <a href="#-1v">Text Link</a>
                                        </li>
                                        <li>
                                            <a href="#-2v">Dynamic Widgets</a>
                                        </li>
                                        <li style="display:none">
                                            <a href="#-3v">Porta gravida</a>
                                        </li>
                                    </ul>
                                    <div id="-1v">
                                        <div id="contactWrapper">
                                        <div id="contactform" class="flv_fullwidth">
                                            <div class="column one column_column" style="padding-bottom:20px;">
                                                    <div class="column_attr ">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                        <div class="column one-half">
                                                            <label>Target Page:</label><span>
                                                                <asp:DropDownList runat="server" ID="ddlTargetPage" style="min-width:120px;" AutoPostBack="true"></asp:DropDownList>
											                    </span>
                                                        </div>
                                                        <div class="column one-half">
                                                            <label> &nbsp;</label><span>
                                                                    <asp:HyperLink runat="server" ID="hypTargetPage" Font-Bold="true" Font-Underline="true" NavigateUrl="~/Withdrawal.aspx" Target="_blank" Text="View link."></asp:HyperLink>
											                    </span>
                                                        </div>
                                                        <div class="column one">
                                                            <label>Ads Name:</label><span>
                                                                    <asp:TextBox runat="server" ID="txtTextAdsName"></asp:TextBox>
											                    </span>
                                                        </div>  
                                                        <div class="column one">
                                                            <label>&nbsp;</label><span>
												                    <asp:LinkButton runat="server" ID="lnkbGenerateTextLink" CssClass="button button_green" Text="Save & Generate Code"></asp:LinkButton>
											                    </span>
                                                        </div>
                                                        <div class="column one">
                                                                    <asp:Button runat="server" ID="btnCopyTextLink" Text="Copy" CssClass="button button_size_3" style="float:right;margin:5px;"/>
                                                                    <div class="code-box">
                                                                        <asp:Label runat="server" ID="lblTextLink" Text="" ToolTip="Place the link in your website." Font-Bold="True" Font-Size="small"></asp:Label>
                                                                    </div>
                                                        </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnCopyTextLink" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="ddlTargetPage" EventName="SelectedIndexChanged"/>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="-2v">
                                        <div id="contactWrapper2">
                                            <div id="dynamicform" class="flv_fullwidth">
                                            <div class="column one column_column" style="padding-bottom:20px;">
                                                    <div class="column_attr ">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                        <div class="column one-third">
                                                            <label>Target Page 1:</label><span>
                                                                <asp:DropDownList runat="server" ID="ddldyntarget1" style="min-width:120px;" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:HyperLink runat="server" ID="hypdyn1" Font-Bold="true" Font-Underline="true" NavigateUrl="~/Withdrawal.aspx" Target="_blank" Text="View link."></asp:HyperLink>
											                    </span>                                                                
                                                        </div>
                                                        <div class="column one-third">
                                                            <label>Target Page 2:</label><span>
                                                                <asp:DropDownList runat="server" ID="ddldyntarget2" style="min-width:120px;" AutoPostBack="true"></asp:DropDownList>
											                    <asp:HyperLink runat="server" ID="hypdyn2" Font-Bold="true" Font-Underline="true" NavigateUrl="~/Withdrawal.aspx" Target="_blank" Text="View link."></asp:HyperLink>
                                                                </span>
                                                        </div>
                                                        <div class="column one-third">
                                                            <label>Target Page 3:</label><span>
                                                                <asp:DropDownList runat="server" ID="ddldyntarget3" style="min-width:120px;" AutoPostBack="true"></asp:DropDownList>
											                    <asp:HyperLink runat="server" ID="hypdyn3" Font-Bold="true" Font-Underline="true" NavigateUrl="~/Withdrawal.aspx" Target="_blank" Text="View link."></asp:HyperLink>    
                                                            </span>
                                                        </div>
                                                        <div class="column one">
                                                            <label>Ads Name:</label><span>
                                                                    <asp:TextBox runat="server" ID="txtDynAdsName" Width="100%"></asp:TextBox>
											                    </span>
                                                        </div>  
                                                        <div class="column one">
                                                            <label>&nbsp;</label><span>
												                    <asp:LinkButton runat="server" ID="lnkbGenerateDynLink" CssClass="button button_green" Text="Save & Generate Code"></asp:LinkButton>
											                    </span>
                                                        </div>
                                                        <div class="column one">
                                                                    <asp:Button runat="server" ID="btnCopyDynLink" Text="Copy" CssClass="button button_size_3" style="float:right;margin:5px;"/>
                                                                    <div class="code-box">
                                                                        <asp:Label runat="server" ID="lblDynLink" Text="" ToolTip="Place the link in your website." Font-Bold="True" Font-Size="small" style="line-height:normal;"></asp:Label>
                                                                    </div>
                                                                    <asp:Panel runat="server" id="pnldisplaydemo" Visible="false">
                                                                        <asp:literal runat="server" id="ltliFrame" />                                                                    
                                                                    </asp:Panel>
                                                        </div>                                                            
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnCopyDynLink" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="ddldyntarget1" EventName="SelectedIndexChanged"/>
                                                                <asp:AsyncPostBackTrigger ControlID="ddldyntarget2" EventName="SelectedIndexChanged"/>
                                                                <asp:AsyncPostBackTrigger ControlID="ddldyntarget3" EventName="SelectedIndexChanged"/>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="-3v">
                                        <p>
                                            <span class="big">Lorem ipsum dolor sit amet enim. Etiam ullamcorper. Suspendisse a pellentesque dui, non felis. Maecenas malesuada elit lectus felis, malesuada ultricies.</span>
                                        </p>
                                        <p>
                                            Donec vestibulum justo a diam ultricies pellentesque. Quisque mattis diam vel lacus tincidunt elementum. Sed vitae adipiscing turpis. Aenean ligula nibh, molestie id viverra a, dapibus at dolor. In iaculis viverra neque, ac eleifend ante lobortis id. In viverra ipsum ac eros tristique dignissim. Donec aliquam velit vitae mi dictum.
                                        </p>
                                    </div>
                                </div>
                            </div>
  
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
             
</asp:Content>
