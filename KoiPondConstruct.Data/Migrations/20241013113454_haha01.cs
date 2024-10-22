using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiPondConstruct.Data.Migrations
{
    /// <inheritdoc />
    public partial class haha01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_img_refer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    img_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    deleted_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_img_refer_id_primary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_price_refer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    currency = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    updated_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    size_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_price_refer_id_primary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sample_design",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    img_id = table.Column<long>(type: "bigint", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    updated_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    content_text = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_sample_design_id_primary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "('NOW')"),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    role = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    avatar_img = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_user_id_primary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sample_design_img_refer",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    sample_design_id = table.Column<long>(type: "bigint", nullable: false),
                    img_refer_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_samp__3213E83FB69DC0EB", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_img_refer",
                        column: x => x.img_refer_id,
                        principalTable: "tbl_img_refer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_sample_design",
                        column: x => x.sample_design_id,
                        principalTable: "tbl_sample_design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer_request",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_cust__3213E83F04C307BE", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_customer_request_user_id",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer_request_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_id = table.Column<long>(type: "bigint", nullable: false),
                    sample_design_id = table.Column<long>(type: "bigint", nullable: false),
                    homeowner_first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    homeowner_last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    homeowner_phone = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    homeowner_date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    height = table.Column<long>(type: "bigint", nullable: false),
                    width = table.Column<long>(type: "bigint", nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    shape = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    budget = table.Column<long>(type: "bigint", nullable: false),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_cust__3213E83FDA2F4631", x => x.id);
                    table.ForeignKey(
                        name: "fk_customer_request_detail_sample_design_id",
                        column: x => x.sample_design_id,
                        principalTable: "tbl_sample_design",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_customer_request_detail_request_id",
                        column: x => x.request_id,
                        principalTable: "tbl_customer_request",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_design",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_detail_id = table.Column<long>(type: "bigint", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    approved_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    content_text = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_design_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_design_request_detail_id",
                        column: x => x.request_detail_id,
                        principalTable: "tbl_customer_request_detail",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_quotation_cost",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_detail_id = table.Column<long>(type: "bigint", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    content_text = table.Column<string>(type: "text", nullable: false),
                    file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false),
                    currency = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_quotation_cost_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_quotation_cost_request_detail_id",
                        column: x => x.request_detail_id,
                        principalTable: "tbl_customer_request_detail",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_suggestion_doc",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sample_design_id = table.Column<long>(type: "bigint", nullable: false),
                    request_detail_id = table.Column<long>(type: "bigint", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    is_first_item = table.Column<bool>(type: "bit", nullable: false),
                    approved_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    approved_tiem = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    content_text = table.Column<string>(type: "text", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_suggestion_doc_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_suggestion_doc_request_detail_id",
                        column: x => x.request_detail_id,
                        principalTable: "tbl_customer_request_detail",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_suggestion_doc_sample_design_id",
                        column: x => x.sample_design_id,
                        principalTable: "tbl_sample_design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_inspection",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    design_id = table.Column<long>(type: "bigint", nullable: false),
                    inspector_id = table.Column<long>(type: "bigint", nullable: false),
                    inspection_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    findings = table.Column<string>(type: "text", nullable: false),
                    recommendations = table.Column<string>(type: "text", nullable: false),
                    severity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_insp__3213E83FA9AF3631", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_inspection_design_id",
                        column: x => x.design_id,
                        principalTable: "tbl_design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_status",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_status_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    design_id = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_final = table.Column<bool>(type: "bit", nullable: false),
                    is_current = table.Column<bool>(type: "bit", nullable: false),
                    evidence_text = table.Column<string>(type: "text", nullable: false),
                    evidence_file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_status_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_status_design_id",
                        column: x => x.design_id,
                        principalTable: "tbl_design",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_price_contract_cost",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quotation_cost_id = table.Column<long>(type: "bigint", nullable: false),
                    price_refer_id = table.Column<long>(type: "bigint", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_price_contract_cost_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_price_contract_cost_price_refer_id",
                        column: x => x.price_refer_id,
                        principalTable: "tbl_price_refer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_price_contract_cost_quotation_cost_id",
                        column: x => x.quotation_cost_id,
                        principalTable: "tbl_quotation_cost",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedback",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    attached_file = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    is_solved = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    suggestion_doc_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tbl_feedback_id_primary", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_feedback_suggestion_doc_id",
                        column: x => x.suggestion_doc_id,
                        principalTable: "tbl_suggestion_doc",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tbl_feedback_user_id",
                        column: x => x.user_id,
                        principalTable: "tbl_user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_inspection_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    inspection_id = table.Column<long>(type: "bigint", nullable: false),
                    attribute = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    observed_value = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    expected_value = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    result = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    comments = table.Column<string>(type: "text", nullable: true),
                    severity = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_insp__3213E83FC6B4C385", x => x.id);
                    table.ForeignKey(
                        name: "fk_tbl_inspection_detail_inspection_id",
                        column: x => x.inspection_id,
                        principalTable: "tbl_inspection",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customer_request_user_id",
                table: "tbl_customer_request",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customer_request_detail_request_id",
                table: "tbl_customer_request_detail",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_customer_request_detail_sample_design_id",
                table: "tbl_customer_request_detail",
                column: "sample_design_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tbl_desi__5A4D1BEA8FACACD6",
                table: "tbl_design",
                column: "request_detail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedback_suggestion_doc_id",
                table: "tbl_feedback",
                column: "suggestion_doc_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_feedback_user_id",
                table: "tbl_feedback",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tbl_insp__1BA5C3FAA0ACCC8F",
                table: "tbl_inspection",
                column: "design_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_inspection_detail_inspection_id",
                table: "tbl_inspection_detail",
                column: "inspection_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_price_contract_cost_price_refer_id",
                table: "tbl_price_contract_cost",
                column: "price_refer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_price_contract_cost_quotation_cost_id",
                table: "tbl_price_contract_cost",
                column: "quotation_cost_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tbl_quot__5A4D1BEA97EC8145",
                table: "tbl_quotation_cost",
                column: "request_detail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sample_design_img_refer_img_refer_id",
                table: "tbl_sample_design_img_refer",
                column: "img_refer_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sample_design_img_refer_sample_design_id",
                table: "tbl_sample_design_img_refer",
                column: "sample_design_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_status_design_id",
                table: "tbl_status",
                column: "design_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_suggestion_doc_request_detail_id",
                table: "tbl_suggestion_doc",
                column: "request_detail_id");

            migrationBuilder.CreateIndex(
                name: "UQ__tbl_sugg__C675143A6D2B672D",
                table: "tbl_suggestion_doc",
                column: "sample_design_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tbl_user_email_unique",
                table: "tbl_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tbl_user_phone_number_unique",
                table: "tbl_user",
                column: "phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tbl_user_username_unique",
                table: "tbl_user",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_feedback");

            migrationBuilder.DropTable(
                name: "tbl_inspection_detail");

            migrationBuilder.DropTable(
                name: "tbl_price_contract_cost");

            migrationBuilder.DropTable(
                name: "tbl_sample_design_img_refer");

            migrationBuilder.DropTable(
                name: "tbl_status");

            migrationBuilder.DropTable(
                name: "tbl_suggestion_doc");

            migrationBuilder.DropTable(
                name: "tbl_inspection");

            migrationBuilder.DropTable(
                name: "tbl_price_refer");

            migrationBuilder.DropTable(
                name: "tbl_quotation_cost");

            migrationBuilder.DropTable(
                name: "tbl_img_refer");

            migrationBuilder.DropTable(
                name: "tbl_design");

            migrationBuilder.DropTable(
                name: "tbl_customer_request_detail");

            migrationBuilder.DropTable(
                name: "tbl_sample_design");

            migrationBuilder.DropTable(
                name: "tbl_customer_request");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
