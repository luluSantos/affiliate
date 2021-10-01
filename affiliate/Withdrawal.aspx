<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Withdrawal.aspx.vb" Inherits="affiliate.Withdrawal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Withdrawal</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Dashboard.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Withdrawal.aspx">Withdrawal</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div class="content_wrapper clearfix">
        <div class="section section-border-top flv_sections_21" id="forms flv_sections_16">
            <div class="section_wrapper clearfix">
                <div class="items_group clearfix">
                        <div class="entry-content">
                            <div class="section" id="table flv_sections_16">
                                <div class="section_wrapper clearfix">
                                    <div class="items_group clearfix">
                                        <!-- One full width row-->
                                        <div class="column one-second column_column">
                                            <div class="column_attr ">
                                                <h5 class="flv_style_4">Withdrawal Records</h5>
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
                                                <asp:GridView ID="gvWR" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                                    <columns>
                                                        <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" SortExpression="Date" />
                                                        <asp:BoundField DataField="Withdrawal" HeaderText="Withdrawal Amount" ReadOnly="True" SortExpression="Withdrawal" />
                                                        <asp:BoundField DataField="Bank" HeaderText="Bank" ReadOnly="True" SortExpression="Withdrawal" />
                                                        <asp:BoundField DataField="BankAcc" HeaderText="Account No" ReadOnly="True" SortExpression="Withdrawal" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status" InsertVisible="False" ReadOnly="True" SortExpression="Status" />
                                                    </columns>
                                                    <EmptyDataTemplate>
                                                        <div align="center">No records found.</div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="column one-second column_column">
                                            <div class="column_attr ">
                                                <h5 class="flv_style_4">Requests Form</h5>
                                            </div>
                                        </div>
                                        <div class="column one column_column">
                                            <div class="column_attr ">
                                                <div id="contactWrapper">
                                                    <!-- One Third (1/3) Column -->
                                                    <div class="column one-third">
                                                        <label>Name:</label><span>
                                                                <asp:TextBox runat="server" ID="txtName" Text="" ReadOnly="true"></asp:TextBox>
												            </span>
                                                    </div>
                                                    <!-- One Third (1/3) Column -->
                                                    <div class="column one-third">
                                                        <label>Amount Available:</label><span>
													            <asp:TextBox runat="server" ID="txtAmountAv" Text="" ReadOnly="true"></asp:TextBox>
												            </span>
                                                    </div>
                                                    <!-- One Third (1/3) Column -->
                                                    <div class="column one-third">
                                                        <label>Withdrawal Request Amount:</label><span>
                                                            <asp:DropDownList runat="server" ID="ddlWithdrawalReqAmount">
                                                                <asp:ListItem Value="0" Text="-Please Select Amount"></asp:ListItem>
                                                                <asp:ListItem Value="100" Text="RM 100.00"></asp:ListItem>
                                                                <asp:ListItem Value="500" Text="RM 500.00"></asp:ListItem>
                                                                <asp:ListItem Value="1000" Text="RM 1000.00"></asp:ListItem>
                                                            </asp:DropDownList>
 												            </span>
                                                    </div>
                                                    <div class="column one">
                                                        <label>Remarks:</label><span>
                                                            <asp:textbox runat="server" ID="txtAFRemark" Text=""></asp:textbox>
                                                    </div>
                                                    <div class="column one">
                                                        <asp:Button runat="server" ID="btnSubmitWRequest" Text="Submit" style="float:right;"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>    
                        </div>
                    </div>


                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"><asp:Label ID="lblWModalTitle" runat="server" Text="Withdrawal"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="lblWModalMessage" runat="server" Text="Test"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnWModalOk" runat="server" Text="Ok" data-dismiss="modal"/>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    </div>
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }
    </script>
</asp:Content>
