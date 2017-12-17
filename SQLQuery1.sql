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