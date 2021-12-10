<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TechnicalTestCasino._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Scripts/DataTable/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/DataTable/jquery.dataTables.min.js"></script>
    <script src="Scripts/DataTable/dataTables.responsive.min.js"></script>
    <script src="Scripts/DataTable/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=gvPlayers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers"
            });
        });
    </script>

    <div class="jumbotron" style="background-color:grey">
        <h1>Players</h1>
        
        <div class="row">
            <div class="mb-3">
                <asp:Label for="txtBoxFName" class="form-label" ID="lblName" runat="server" Text="First name:"></asp:Label>
                <asp:TextBox ID="txtBoxFName" class="form-control" 
                            placeholder="First name" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label for="txtBoxFaName" class="form-label" ID="lblFName" runat="server" Text="Father name:"></asp:Label>
                <asp:TextBox ID="txtBoxFaName" class="form-control" 
                        placeholder="Father name" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label for="txtBoxMName" class="form-label" ID="lblMName" runat="server" Text="Mother name:"></asp:Label>
                <asp:TextBox ID="txtBoxMName" class="form-control" 
                        placeholder="Mother name" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label for="txtBoxAge" class="form-label" ID="lblAge" runat="server" Text="Age:"></asp:Label>
                <asp:TextBox ID="txtBoxAge" class="form-control"  type="number"
                        placeholder="Age" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="padding:10px">
            <asp:Button CssClass="btn btn-primary" ID="btnAddPlayer" runat="server" 
                        Text="Add New Player" OnClick="btnAddPlayer_Click" />
        </div>
    </div>

    <div class="row" style="background-color:lightgrey" id="MessageEmpty" runat="server" visible="false">
        <%--<asp:Label ID="lblEmpty" runat="server" Text="The table is empty, record some players :)"></asp:Label>--%>
        <div style="margin-left: auto; margin-right: auto; text-align: center;">
            <asp:Label ID="lblEmpty" runat="server" Text="The table is empty, record some players :)" Font-Bold="true" Font-Size="X-Large"
                CssClass="StrongText"></asp:Label>
        </div>
    </div>

    <div class="row" id="gridDiv" runat="server" visible="false">

        <asp:UpdatePanel ID="upGvPlayers" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView class="table table-bordered table-condensed table-responsive table-hover" 
                            ID="gvPlayers" runat="server" autogeneratecolumns="False" DataKeyNames="PlayerId" 
                            OnRowDeleting="gvPlayers_RowDeleting" OnSelectedIndexChanged="gvPlayers_SelectedIndexChanged">
                    <Columns>
                        <asp:ButtonField HeaderText="Action" Text="Delete" CommandName="Delete" ItemStyle-Width="150" />
                        <asp:ButtonField HeaderText="Action" Text="Select" CommandName="Select" ItemStyle-Width="150" />
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="PlayerId" HeaderText="" Visible="false" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="MiddleName" HeaderText="Father Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Mother Name" />
                        <asp:BoundField DataField="Age" HeaderText="Age" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="modal fade" id="EditPlayerModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><asp:Label ID="lblModalTitle" runat="server" Text="Edit Player Information"></asp:Label></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="margin-left:30px">
                                <div class="mb-3">
                                    <asp:HiddenField ID="hdnId" runat="server" />
                                    <asp:Label for="txtEditName" class="form-label" ID="lblEditName" runat="server" Text="First name:"></asp:Label>
                                    <asp:TextBox ID="txtEditName" class="form-control" 
                                                placeholder="First name" runat="server"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label for="txtEditFName" class="form-label" ID="lblEditFName" runat="server" Text="Father name:"></asp:Label>
                                    <asp:TextBox ID="txtEditFName" class="form-control" 
                                            placeholder="Father name" runat="server"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label for="txtEditMName" class="form-label" ID="lblEditMName" runat="server" Text="Mother name:"></asp:Label>
                                    <asp:TextBox ID="txtEditMName" class="form-control" 
                                            placeholder="Mother name" runat="server"></asp:TextBox>
                                </div>
                                <div class="mb-3">
                                    <asp:Label for="txtEditAge" class="form-label" ID="lblEditAge" runat="server" Text="Age:"></asp:Label>
                                    <asp:TextBox ID="txtEditAge" class="form-control" type="number"
                                            placeholder="Age" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCloseModal" class="btn btn-primary" runat="server" Text="Close" OnClick="btnCloseModal_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="MyAlertModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upAlert" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><asp:Label ID="lblAlert" runat="server" Text="Alert Message"></asp:Label></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row" style="margin-left:30px">
                                <asp:Label ID="lblMessageAlert" runat="server" Text="">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
