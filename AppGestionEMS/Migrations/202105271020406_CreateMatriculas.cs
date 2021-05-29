namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMatriculas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculacions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CursoId, t.GrupoId })
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matriculacions", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.Matriculacions", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Matriculacions", new[] { "GrupoId" });
            DropIndex("dbo.Matriculacions", new[] { "CursoId" });
            DropIndex("dbo.Matriculacions", new[] { "UserId" });
            DropTable("dbo.Matriculacions");
        }
    }
}
