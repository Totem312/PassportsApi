CREATE OR REPLACE function passport_insert_update_tg()
returns  trigger
as $$
begin 
	if(TG_OP='INSERT') then 
insert into public.history(passport_id,status,date_change_status) values (new."Id" ,new.status,now());
return new;
elseif(TG_OP='update') then
insert into public.history(passport_id,status,date_change_status) values (new."Id" ,new.status,now());
return new;
end if;
end ;
$$language plpgsql