use PanamaDB;
select * from [dbo].[EntitysAll]
where name='12020428'
select * from [dbo].[Connections]
 --where nameFrom='12020428'
 where nameTo='14086288'

--delete  from [dbo].[EntitysAll]
--delete  from [dbo].[Connections]
--update CountryItems set tStatus=0

select * from v_noCountryForAdd