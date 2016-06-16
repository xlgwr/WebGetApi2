use PanamaDB;
select * from (select distinct getPage from CountryItems
WHERE   (CountriesIndex = 38)) b
ORDER BY getPage DESC -- where tStatus<>;
--delete from CountryItems

select * from CountryItems 
where name='11011370'

select Countries,CountriesIndex,COUNT(Countries) from CountryItems
group by Countries,CountriesIndex
order by Countries

select top 1000 * from Connections order by addDate desc;
select top 1000 * from EntitysAll order by addDate desc;

select count(*) from CountryItems;

select count(*) from Connections;
select count(*) from EntitysAll;


select * from v_noCountryForAdd;

select * from EntitysAll where status='Dead' and DormDate is null and CompanyType is null

--update CountryItems set tStatus=0 from CountryItems where name in (select name from EntitysAll where status='Dead' and DormDate is null and CompanyType is null and ClientIP='192.168.1.6')
--delete from EntitysAll where status='Dead' and DormDate is null and CompanyType is null and ClientIP='192.168.1.6'
