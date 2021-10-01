<%@ Page Title="Performance" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Performance.aspx.vb" Inherits="affiliate.Performance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Performance</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Dashboard.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Performance.aspx">Performance</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div class="content_wrapper clearfix">
                <div class="sections_group">
                    <div class="entry-content">
                        <div class="section section-border-top flv_sections_21" id="tabs">
                            <div class="section_wrapper clearfix">
                                <div class="items_group clearfix">
                                    <div class="column one column_column">
                                        <div class="column_attr ">
                                            <h5 class="flv_style_4">Overview</h5>
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
                                                                <asp:label runat="server" ID="lbl7clks" class="number" data-to="0">0</asp:label> </div>
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
                                                                <asp:label runat="server" ID="lbl7ads" class="number" data-to="0">0</asp:label> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Ads </p>
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
                                                                <asp:label runat="server" ID="lbl30clks" class="number" data-to="0">0</asp:label> </div>
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
                                                                <asp:label runat="server" ID="lbl30ads" class="number" data-to="0">0</asp:label> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Ads </p>
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
                                                                <asp:label runat="server" ID="lbl90clks" class="number" data-to="0">0</asp:label> </div>
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
                                                                <asp:label runat="server" ID="lbl90ads" class="number" data-to="0">0</asp:label> </div>
                                                            <p class="title" style="color:black;">
                                                                Total Ads </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="section" id="table flv_sections_16">
                            <div class="section_wrapper clearfix">
                                <div class="items_group clearfix">
                                    <!-- One full width row-->
                                    <div class="column one-second column_column">
                                        <div class="column_attr ">
                                            <h5 class="flv_style_4">Details</h5>
                                        </div>
                                    </div>
                                    <div class="column one-second column_column">
                                        <div class="column_attr ">
                                            <asp:Button runat="server" ID="btnExport" Text="Export Excel" style="float:right;"/>
                                        </div>
                                    </div>
                                    <!-- One Second (1/2) Column -->
                                    <div class="column one column_column">
                                        <div class="column_attr ">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" >
                                                <columns>
                                                    <asp:BoundField DataField="Method" HeaderText="Method" InsertVisible="False" ReadOnly="True" SortExpression="Method" />
                                                    <asp:BoundField DataField="AdsName" HeaderText="Name" InsertVisible="False" ReadOnly="True" SortExpression="AdsName" />
                                                    <asp:BoundField DataField="CPC/CPL" HeaderText="CPC/CPL" InsertVisible="False" ReadOnly="True" SortExpression="CPC/CPL" />
                                                    <asp:BoundField DataField="Earning" HeaderText="Earning" InsertVisible="False" ReadOnly="True" SortExpression="Earning" />
                                                    <asp:BoundField DataField="CreatedDate" HeaderText="Date" InsertVisible="False" ReadOnly="True" SortExpression="CreatedDate" />
                                                </columns>
                                                <EmptyDataTemplate>
                                                    <div align="center">No records found.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>                    

                    </div>
                </div>
            </div>
</asp:Content>
