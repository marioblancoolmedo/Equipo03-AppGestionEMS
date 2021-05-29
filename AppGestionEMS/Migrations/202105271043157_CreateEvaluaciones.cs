namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEvaluaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluacions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                        GradeEx = c.Single(nullable: false),
                        GradePr = c.Single(nullable: false),
                        TypeConvocatoria = c.Int(nullable: false),
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
            DropForeignKey("dbo.Evaluacions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Evaluacions", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.Evaluacions", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Evaluacions", new[] { "GrupoId" });
            DropIndex("dbo.Evaluacions", new[] { "CursoId" });
            DropIndex("dbo.Evaluacions", new[] { "UserId" });
            DropTable("dbo.Evaluacions");
        }
    }
}
