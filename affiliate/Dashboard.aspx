<%@ Page Title="Dashboard" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Dashboard.aspx.vb" Inherits="affiliate.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Dashboard</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Default.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Dashboard.aspx">Dashboard</a>
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
                                        <div class="column_attr ">
                                           <h5 class="title"><asp:Label runat="server" ID="lblDashboardWelcome" ></asp:Label> <span style="font-size:8px"><asp:Label runat="server" ID="lblDashboardWelcome2" ></asp:Label></span></h5>
                                        </div>
                                    </div>
                                    <!-- One Third (1/3) Column -->
                                    <div class="column one-third column_list">
                                        <div class="list_item lists_2 clearfix">
                                            <a href="#">
                                                <div class="list_left list_image"><img src="Assets/images/lawyer_list_1.png" alt="Tincidunt mauris" class="scale-with-grid" />
                                                </div>
                                                <div class="list_right">
                                                    <h4>Tincidunt mauris</h4>
                                                    <div class="desc">
                                                        Vitae adipiscing turpis. Aenean ligula nibh in, molestie id viverra a, dapibus at dolor.
                                                    </div>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                    <!-- One Third (1/3) Column -->
                                    <div class="column one-third column_list">
                                        <div class="list_item lists_2 clearfix">
                                            <a href="#">
                                                <div class="list_left list_image"><img src="Assets/images/lawyer_list_2.png" alt="Integer aliquam" class="scale-with-grid" />
                                                </div>
                                                <div class="list_right">
                                                    <h4>Integer aliquam</h4>
                                                    <div class="desc">
                                                        Vitae adipiscing turpis. Aenean ligula nibh in, molestie id viverra a, dapibus at dolor.
                                                    </div>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                    <!-- One Third (1/3) Column -->
                                    <div class="column one-third column_list">
                                        <div class="list_item lists_2 clearfix">
                                            <a href="#">
                                                <div class="list_left list_image"><img src="Assets/images/lawyer_list_3.png" alt="Malesuada fames" class="scale-with-grid" />
                                                </div>
                                                <div class="list_right">
                                                    <h4>Malesuada fames</h4>
                                                    <div class="desc">
                                                        Vitae adipiscing turpis. Aenean ligula nibh in, molestie id viverra a, dapibus at dolor.
                                                    </div>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                    <!-- Page devider -->
                                    <!-- One full width row-->
                                    <div class="column one column_divider">
                                        <hr class="no_line flv_style_5" />
                                    </div>                                  
                                </div>
                            </div>
                        </div>
                        <div class="section" id="columns flv_sections_16">
                            <div class="section_wrapper clearfix">
                                <div class="items_group clearfix">
                                    <!-- One full width row-->
                                    <div class="column one column_column">
                                        <div class="column_attr ">
                                            <h5 class="flv_style_4">Info</h5>
                                        </div>
                                    </div>
                                    <!-- One Second (1/2) Column -->
                                    <div class="column one-second column_column" style="background-color:lemonchiffon;">
                                        <div style="padding: 20px;min-height: 75px;">
                                            <div class="column one-second "><h5>Affiliate Level</h5>
                                                </div>
                                            <div class="column one-second "><span style="color:black">Acquired Clicks :<asp:Label runat="server" Font-Size="Large" Text=" 12/100"></asp:Label></span><br /><span style="color:black">Current Level : <asp:Label runat="server" Font-Size="Large" Text=" 1"></asp:Label></span><br /><span style="float:right;""><a href="#"><i class="icon-forward"></i>More</a></span>
                                                </div>                                           
                                        </div>
                                    </div>
                                    <!-- One Second (1/2) Column -->
                                    <div class="column one-second column_column" style="background-color:lemonchiffon;">
                                        <div style="padding: 20px;min-height: 75px;">
                                            <div class="column one-second "><h5>Earnings</h5>
                                                </div>
                                            <div class="column one-second "><span style="color:black">Current Balance :<asp:Label runat="server" Font-Size="Large" Text=" RM 12"></asp:Label></span><br /><span style="color:black">Most Recent Payment : <asp:Label runat="server" Font-Size="Large" Text=" RM 12"></asp:Label></span><br /><span style="float:right;""><a href="#"><i class="icon-forward"></i>Withdrawal</a></span>
                                                </div>                                            
                                        </div>
                                    </div>                                  
                                </div>
                            </div>
                        </div>  
                        <div class="section" id="tabs flv_sections_16">
                            <div class="section_wrapper clearfix">
                                <div class="items_group clearfix">
                                    <div class="column one column_column">
                                        <div class="column_attr ">
                                            <h5 class="flv_style_4"></h5>
                                        </div>
                                    </div>
                                    <div class="column one column_tabs">
                                        <div class="jq-tabs tabs_wrapper tabs_horizontal">
                                            <ul>
                                                <li>
                                                    <a href="#-1v">Last 7 Days</a>
                                                </li>
                                                <li>
                                                    <a href="#-2v">Last 30 Days</a>
                                                </li>
                                                <li>
                                                    <a href="#-3v">Last 90 Days</a>
                                                </li>
                                            </ul>
                                            <div id="-1v">
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-branch"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Clicks </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-tree"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Referal </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="-2v">
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-branch"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Clicks </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-tree"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Referal </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="-3v">
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-branch"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Clicks </p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- One Third (1/3) Column -->
                                                <div class="column one-second column_counter" style="margin-bottom:0">
                                                    <div class="counter animate-math counter_horizontal">
                                                        <div class="icon_wrapper">
                                                            <i class="icon-flow-tree"></i> </div>
                                                        <div class="desc_wrapper">
                                                            <div class="number-wrapper">
                                                                <span class="number" data-to="0">0</span> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Referal </p>
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
            </div>

</asp:Content>
