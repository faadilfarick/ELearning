create proc AddVideos
(
@name nvarchar(max),
@dis nvarchar(max),
@cID int,
@path nvarchar(max)
)
as
begin
insert into [dbo].[Videos]([Name],[Discription],[Course_ID],[FilePath]) values(@name,@dis,@cID,@path)
end

go

create proc UpdateVideos
(
@id int,
@name nvarchar(max),
@dis nvarchar(max),
@cID int
)
as
begin
update [dbo].[Videos] set [Name]=@name,[Discription]=@dis,[Course_ID]=@cID where [ID]=@id
end
go

create proc UpdateVideosWithFile
(
@id int,
@name nvarchar(max),
@dis nvarchar(max),
@cID int,
@path nvarchar(max)
)
as
begin
update [dbo].[Videos] set [Name]=@name,[Discription]=@dis,[Course_ID]=@cID,[FilePath]=@path  where [ID]=@id
end

--SELECT * FROM [dbo].[Videos] 
-- SELECT * FROM [dbo].[Courses] where Courses.ID=1
-- select* from [dbo].[AspNetUsers]
go

 create proc AddCourse
 (
 @name nvarchar(max),
 @discription nvarchar(max),
 @uID nvarchar(max)
 )
 as
 begin
 insert into [dbo].[Courses]([Name],[Discription],[ApplicationUser_Id]) values (@name,@discription,@uID)
 end
 go

create proc getRoleForUserId(
@UID nvarchar(max)
)
as
begin

SELECT        AspNetRoles.Name
FROM            AspNetRoles INNER JOIN
                         AspNetUserRoles ON AspNetRoles.Id = AspNetUserRoles.RoleId INNER JOIN
                         AspNetUsers ON AspNetUserRoles.UserId = AspNetUsers.Id	where AspNetUsers.Id=@UID

 
 end

 go
 create proc addSubCategories
 (
 @Name nvarchar(max),
 @Discription nvarchar(max),
 @CategoriID int
 )
 as
 begin
 insert into [dbo].[SubCategories]([Name],[Discription],[Category_ID]) values(@Name,@Discription,@CategoriID)
 end
 go

create proc UpdateSubCategories
 (
 @id int,
 @Name nvarchar(max),
 @Discription nvarchar(max),
 @CategoriID int
 )
 as
 begin
 Update [dbo].[SubCategories] set[Name]= @Name,[Discription]=@Discription,[Category_ID]=@CategoriID where[ID]=@id
 end

 go
 create proc GetSubCategoriesForListItem 
 (
 @id int
 )
 as
 begin
  select * from [dbo].[SubCategories] where [Category_ID]=@id
  end



GO
/****** Object:  StoredProcedure [dbo].[AddCourse]    Script Date: 12/18/2017 11:19:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER proc [dbo].[AddCourse]
 (
 @name nvarchar(max),
 @discription nvarchar(max),
 @uID nvarchar(max),
 @price decimal(18,2),
 @image nvarchar(max),
 @MainCat int,
 @subCat int
 )
 as
 begin
 insert into [dbo].[Courses]([Name],[Discription],[ApplicationUser_Id],[Price],[Image],[MainCategory_ID],[SubCategory_ID]) 
 values (@name,@discription,@uID,@price,@image,@MainCat,@subCat)
 end
 
Go


 create proc UpdateCourse
 (
 @id int,
  @name nvarchar(max),
 @discription nvarchar(max),
 @price decimal(18,2),
 @image nvarchar(max)
 )
 as
 begin
 update [dbo].[Courses]  set [Name]=@name,[Discription]=@discription,[Price]=@price,[Image]=@image where[ID]=@id
 end

 

GO
/****** Object:  StoredProcedure [dbo].[UpdateCourse]    Script Date: 12/19/2017 11:46:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER proc [dbo].[UpdateCourse]
 (
 @id int,
  @name nvarchar(max),
 @discription nvarchar(max),
 @price decimal(18,2),
 @image nvarchar(max),
 @MainCat int,
 @subCat int
 )
 as
 begin
 update [dbo].[Courses]  set [Name]=@name,[Discription]=@discription,[Price]=@price,[Image]=@image ,[MainCategory_ID]=@MainCat,[SubCategory_ID]=@subCat where[ID]=@id
 end

 GO
/****** Object:  StoredProcedure [dbo].[AddCourse]    Script Date: 12/21/2017 3:32:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER proc [dbo].[AddCourse]
 (
 @name nvarchar(max),
 @discription nvarchar(max),
 @uID nvarchar(max),
 @price decimal(18,2),
 @image nvarchar(max),
 @MainCat int,
 @subCat int
 )
 as
 begin
 insert into [dbo].[Courses]([Name],[Discription],[ApplicationUser_Id],[Price],[Image],[MainCategory_ID],[SubCategory_ID]) 
 values (@name,@discription,@uID,@price,@image,@MainCat,@subCat)

 declare @Cid int

 select @Cid= [ID] from [dbo].[Courses] where [Name]=@name
 insert into [dbo].[Fora]([CourseTitle],[Description],[Course_ID])values(@name,@discription,@Cid)
 end

 GO
/****** Object:  StoredProcedure [dbo].[UpdateCourse]    Script Date: 12/21/2017 3:40:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER proc [dbo].[UpdateCourse]
 (
 @id int,
  @name nvarchar(max),
 @discription nvarchar(max),
 @price decimal(18,2),
 @image nvarchar(max),
 @MainCat int,
 @subCat int
 )
 as
 begin
 update [dbo].[Courses]  set [Name]=@name,[Discription]=@discription,[Price]=@price,[Image]=@image ,[MainCategory_ID]=@MainCat,[SubCategory_ID]=@subCat where[ID]=@id

 update [dbo].[Fora] set [CourseTitle]=@name,[Description]=@discription where [Course_ID]=@id
 end



