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

select count(*) from CountryItems;

select count(*) from Connections;
select count(*) from EntitysAll;


select * from v_noCountryForAdd
