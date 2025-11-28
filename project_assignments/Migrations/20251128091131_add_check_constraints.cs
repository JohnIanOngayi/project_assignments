using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_assignments.Migrations
{
    /// <inheritdoc />
    public partial class add_check_constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // CHECK constraint for AllocationPercent (must be > 0 and <= 100)
            migrationBuilder.Sql(@"
                ALTER TABLE ProjectAssignments 
                ADD CONSTRAINT CK_ProjectAssignments_AllocationPercent 
                CHECK (AllocationPercent > 0 AND AllocationPercent <= 100)
            ");

            // CHECK constraint for DateRange (EndDate >= StartDate)
            migrationBuilder.Sql(@"
                ALTER TABLE ProjectAssignments 
                ADD CONSTRAINT CK_ProjectAssignments_DateRange 
                CHECK (StartDate IS NULL OR EndDate IS NULL OR EndDate >= StartDate)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Detect database provider
            var isMySql = migrationBuilder.ActiveProvider?.Contains("MySql") ?? false;

            if (isMySql)
            {
                // MySQL syntax
                migrationBuilder.Sql(@"
                    ALTER TABLE ProjectAssignments 
                    DROP CHECK CK_ProjectAssignments_AllocationPercent
                ");

                migrationBuilder.Sql(@"
                    ALTER TABLE ProjectAssignments 
                    DROP CHECK CK_ProjectAssignments_DateRange
                ");
            }
            else
            {
                // SQL Server syntax
                migrationBuilder.Sql(@"
                    ALTER TABLE ProjectAssignments 
                    DROP CONSTRAINT CK_ProjectAssignments_AllocationPercent
                ");

                migrationBuilder.Sql(@"
                    ALTER TABLE ProjectAssignments 
                    DROP CONSTRAINT CK_ProjectAssignments_DateRange
                ");
            }
        }
    }
}
