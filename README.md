# NotesMVC

- The project is built with Entity Framework 6 using ASP.NET MVC.

- SQL query to build the database in NotesMVC\GloBirdEnergy\SQL Queries\CreateTables.sql. 
  Rollback query: NotesMVC\GloBirdEnergy\SQL Queries\Rollback_CreateTables.sql.

- There are some validation logic in BOL model classes: NotesMVC\GloBirdEnergy\BOL\Customer.cs and CallNote.cs (TODO: should be refactored into BLL)
  EF6 may overwrite those 2 classes so backup them is a work-around.
  
- Log file is created in NotesMVC\GloBirdEnergy\UI\logs

I had fun coding this and have learnt a lot during this process, thank you!
