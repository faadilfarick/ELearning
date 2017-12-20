create proc AddVideos
(
@name nvarchar(max),
@dis nvarchar(max),
@cID int
@path nvarchar(max)
)
as
begin
insert into [dbo].[Videos]([Name],[Discription],[Course_ID],[FilePath]) values(@name,@dis,@cID,@path)
end


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


 create proc GetSubCategoriesForListItem 1
 (
 @id int
 )
 as
 begin
  select * from [dbo].[SubCategories] where [Category_ID]=@id
  end



  USE [aspnet-ELearningDB]
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



 USE [aspnet-ELearningDB]
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


