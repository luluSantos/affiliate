<%@ Page Title="Registration" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Registration.aspx.vb" Inherits="affiliate.Registration" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--Subheader area - only for certain pages -->
    <div id="Subheader">
        <div class="container">
            <div class="column one">
                <h1 class="title">Registration</h1>
                <!--BreadCrumbs area-->
                <ul class="breadcrumbs">
                    <li>
                        <a href="Default.aspx">Home</a><span><i class="icon-right-open"></i></span>
                    </li>
                    <li>
                        <a href="Registration.aspx">Registration</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="content_wrapper clearfix">
        <div class="sections_group">
            <div class="entry-content">
                <div class="section section-border-top flv_sections_21">
                    <div class="section_wrapper clearfix">
                        <div class="items_group clearfix">
                            <!-- One Third (1/3) Column -->
                            <div class="column one-third column_column">
                                <div class="column_attr ">
                                    <div class="flv_style_11">
                                        <h2>Register Here.</h2>
                                        <p>
                                            <span class="big"></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <!-- Two Third (2/3) Column -->
                            <div class="column two-third column_column">
                                <div class="column_attr ">
                                    <div id="contactWrapper">
                                            <!-- One Third (1/3) Column -->
                                            <div class="column one-half">
                                                <label><span style="color:red;">*</span>First Name:</label><span>
                                                        <asp:TextBox runat="server" ID="txtRegFirstName" size="40"></asp:TextBox>
											        </span>
                                                <asp:RequiredFieldValidator ID="rfvRegFirstName" runat="server" ControlToValidate="txtRegFirstName" ErrorMessage="First Name cannot be empty." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="column one-half">
                                                <label><span style="color:red;">*</span>Last Name:</label><span>
                                                        <asp:TextBox runat="server" ID="txtRegLastName" size="40"></asp:TextBox>
											        </span>
                                                <asp:RequiredFieldValidator ID="rfvRegLastName" runat="server" ControlToValidate="txtRegLastName" ErrorMessage="Last Name cannot be empty." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>                                           
                                            <!-- One Third (1/3) Column -->
                                            <div class="column one">
                                                <label>Company Name: <span style="font-size:10px;font-weight:900"> (Optional)</span></label><span>
												        <asp:TextBox runat="server" ID="txtRegComName" size="40"></asp:TextBox>
											        </span>
                                            </div>
                                            <!-- One Third (1/3) Column -->
                                            <div class="column one">
                                                <label><span style="color:red;">*</span>E-mail address:</label><span>
                                                        <asp:TextBox runat="server" ID="txtRegEmail" size="40" TextMode="Email"></asp:TextBox>
											        </span>
                                                <asp:RequiredFieldValidator ID="rfvRegEmail" runat="server" ControlToValidate="txtRegEmail" ErrorMessage="Email cannot be empty." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revRegEmail" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtRegEmail" ErrorMessage="Invalid Email Format" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                            <!-- One Third (1/3) Column -->
                                            <div class="column one">
                                                <label><span style="color:red;">*</span>Password:</label><span>
                                                        <asp:TextBox runat="server" ID="txtRegPassword" size="40" TextMode="Password"></asp:TextBox>
											        </span>
                                                <asp:RequiredFieldValidator ID="rfvRegPassword" runat="server" ControlToValidate="txtRegPassword" ErrorMessage="Password cannot be empty." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revRegPassword" runat="server" ValidationExpression="^.*(?=.{8,})(?=.*[\d]).*$" ControlToValidate="txtRegPassword" ErrorMessage="Password must be alphanumerical and contain at least 8 characters." ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                            <div class="column one">
                                                    <asp:CheckBox ID="cbRegAgreement" runat="server" AutoPostback="true"/>
                                                    <asp:Label runat="server" CssClass="indented-checkbox-text" Text="I have read and agree to mHartanah's Affiliate Agreement and Privacy Policy and consent to my personal data being processed in accordance with mHartanah's Privacy Policy." Font-Size="Smaller" ></asp:Label>
                                            </div>                                       
                                            <div class="column one align_right">
                                                    <asp:Button runat="server" ID="btnSubmitReg" Text="Submit" Enabled="false" BackColor="Gray" />
                                            </div>
                                            <div class="modal fade" id="myModal" role="dialog">
                                                <div class="modal-dialog modal-dialog-centered" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                            <h4 class="modal-title"><asp:Label ID="lblRegModalTitle" runat="server" Text="Registration"></asp:Label></h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:Label ID="lblRegModalMessage" runat="server" Text="Test"></asp:Label>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Button ID="btnRegModalOk" runat="server" Text="Ok"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cbRegAgreement" EventName="CheckedChanged"/>
                                            </Triggers>
                                            </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="section section-border-top flv_sections_21">
                            <div class="section_wrapper clearfix">
                                <div class="items_group clearfix">
                                    <!-- One Fourth (1/4) Column -->
                                    <div class="column one-fourth column_how_it_works">
                                        <div class="how_it_works has_border">
                                            <div class="image"><img src="Assets/images/how_it_works_1.png" class="scale-with-grid" alt="Step 1"><span class="number">1</span>
                                            </div>
                                            <h4>Step 1</h4>
                                            <div class="desc">
                                                Vitae adipiscing turpis. Aenean ligula nibh, molestie id viverra a, dapibus at dolor.
                                            </div>
                                        </div>
                                    </div>
                                    <!-- One Fourth (1/4) Column -->
                                    <div class="column one-fourth column_how_it_works">
                                        <div class="how_it_works has_border">
                                            <div class="image"><img src="Assets/images/how_it_works_2.png" class="scale-with-grid" alt="Step 2"><span class="number">2</span>
                                            </div>
                                            <h4>Step 2</h4>
                                            <div class="desc">
                                                Vitae adipiscing turpis. Aenean ligula nibh, molestie id viverra a, dapibus at dolor.
                                            </div>
                                        </div>
                                    </div>
                                    <!-- One Fourth (1/4) Column -->
                                    <div class="column one-fourth column_how_it_works">
                                        <div class="how_it_works has_border">
                                            <div class="image"><img src="Assets/images/how_it_works_3.png" class="scale-with-grid" alt="Step 3"><span class="number">3</span>
                                            </div>
                                            <h4>Step 3</h4>
                                            <div class="desc">
                                                Vitae adipiscing turpis. Aenean ligula nibh, molestie id viverra a, dapibus at dolor.
                                            </div>
                                        </div>
                                    </div>
                                    <!-- One Fourth (1/4) Column -->
                                    <div class="column one-fourth column_how_it_works">
                                        <div class="how_it_works no_border">
                                            <div class="image"><img src="Assets/images/how_it_works_4.png" class="scale-with-grid" alt="Step 4"><span class="number">4</span>
                                            </div>
                                            <h4>Step 4</h4>
                                            <div class="desc">
                                                Vitae adipiscing turpis. Aenean ligula nibh, molestie id viverra a, dapibus at dolor.
                                            </div>
                                        </div>
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
