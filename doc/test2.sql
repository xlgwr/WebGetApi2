use PanamaDB;
select * from [dbo].[EntitysAll]
where name='12020428'
select * from [dbo].[Connections]
 where nameFrom='12020428'
 --where nameTo='12031453'

--delete  from [dbo].[EntitysAll]
--delete  from [dbo].[Connections]
--update CountryItems set tStatus=0

select m.*,n.Tid from [dbo].[v_noCountryName] m left join dbo.EntitysAll n 
on m.name=n.name
where n.Tid is null