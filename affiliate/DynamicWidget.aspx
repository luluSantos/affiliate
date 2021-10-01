<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DynamicWidget.aspx.vb" Inherits="affiliate.DynamicWidget" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>mHartanah</title>
    <link rel='stylesheet' href='Assets/css/global.css' />
    <link rel='stylesheet' href='Assets/css/structure.css'/>
    <link rel='stylesheet' href='Assets/css/be_style.css'/>
    <base target="_parent" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="section mcb-section" id="articlebox" style="padding-top:50px; padding-bottom:0px">
            <div class="section_wrapper mcb-section-inner">
                <div class="wrap mcb-wrap one valign-top clearfix">
                    <div class="mcb-wrap-inner">
                        <div class="column mcb-column one-third column_article_box ">
                            <div class="article_box">
                                <asp:HyperLink runat="server" ID="hyp0" NavigateUrl="#" alt="img">
                                    <div class="photo_wrapper">
                                        <asp:Image runat="server" ID="img0" ImageUrl="#" AlternateText="img" style="max-height:150px;" />
                                    </div>
                                    <div class="desc_wrapper">
                                        <p style="font-size:small">
                                            <asp:Label runat="server" ID="text0"></asp:Label>
                                        </p>
                                        <h5><asp:Label runat="server" ID="title0"></asp:Label></h5><i class="icon-right-open themecolor">Available Now @MHartanah !</i>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                        <div class="column mcb-column one-third column_article_box ">
                            <div class="article_box">
                                <asp:HyperLink runat="server" ID="hyp1" NavigateUrl="#">
                                    <div class="photo_wrapper">
                                        <asp:Image runat="server" ID="img1" ImageUrl="#" AlternateText="img" style="max-height:150px;"/>
                                    </div>
                                    <div class="desc_wrapper">
                                        <p style="font-size:small">
                                            <asp:Label runat="server" ID="text1"></asp:Label>
                                        </p>
                                        <h5><asp:Label runat="server" ID="title1"></asp:Label></h5><i class="icon-right-open themecolor">Available Now @MHartanah !</i>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                        <div class="column mcb-column one-third column_article_box ">
                            <div class="article_box">
                                <asp:HyperLink runat="server" ID="hyp2" NavigateUrl="#">
                                    <div class="photo_wrapper">
                                        <asp:Image runat="server" ID="img2" ImageUrl="#" AlternateText="img" style="max-height:150px;"/>
                                    </div>
                                    <div class="desc_wrapper">
                                        <p style="font-size:small">
                                            <asp:Label runat="server" ID="text2"></asp:Label>
                                        </p>
                                        <h5><asp:Label runat="server" ID="title2"></asp:Label></h5><i class="icon-right-open themecolor">Available Now @MHartanah !</i>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
