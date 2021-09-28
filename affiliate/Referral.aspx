<%@ Page Title="Referral" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Referral.aspx.vb" Inherits="affiliate.Referral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Referral</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Default.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Referral.aspx">Referral</a>
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
                            <div class="column one column_accordion">
                                <div class="accordion">
                                    <div class="mfn-acc accordion_wrapper open1st">
                                        <div class="question">
                                            <div class="title">
                                                <i class="icon-plus acc-icon-plus"></i><i class="icon-minus acc-icon-minus"></i>Direct Link
                                            </div>
                                            <div class="answer">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="btnCopyTextLink" Text="Copy" style="float:right;margin:5px;"/>
                                                            <div class="code-box">
                                                                <asp:Label runat="server" ID="lblTextLink" Text="https://Test" ToolTip="Place the HTML link in your website." Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnCopyTextLink" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="question">
                                            <div class="title">
                                                <i class="icon-plus acc-icon-plus"></i><i class="icon-minus acc-icon-minus"></i>Dynamic Banner
                                            </div>
                                            <div class="answer">
                                                <script src="mhAffliate.js" x="4100300030003000300030003100"></script>
                                                <br />
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="btnCopyBannerLink" Text="Copy" style="float:right;margin:5px;"/>
                                                            <div class="code-box">
                                                                <asp:Label runat="server" ID="lblBannerCode" Text="" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnCopyTextLink" EventName="Click" />
                                                        </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
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
