REM this should generate a new Pic.DAL.DataContext.designer.sqlserver.cs from the PicParamDb.dbml file.
"K:\Codesion\PicSharp\ThirdPartyComponents\DbMetal.exe" "K:\Codesion\PicSharp\Pic.DAL\PicParamDb.dbml" /server:".\SQLEXPRESS" /database:"PicParamDb" /provider:"sqlserver" /code:"Pic.DAL.DataContext.designer.sqlserver.cs" /language:C# /namespace:"Pic.DAL"