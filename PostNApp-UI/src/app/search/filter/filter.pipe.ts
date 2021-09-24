import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ 
    name: 'Filter'
 })
export class FilterPipe implements PipeTransform {

  transform(value: any, searchInput: string) {
    if (value.length === 0){
        return value;
    }


  const users = [];
    for (const user of value){
        if (user['firstName'] === searchInput || user['lastName']===searchInput || user['username']===searchInput){
            users.push(user);
        }
    }
    return users;
}
  

}
