

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ColorType } from 'src/enum/colortype.enum'
import { QuantityPersonByColor } from 'src/model/quantitypersonbycolor.model';

const apiUrl = 'https://localhost:44327/api/personconsumer';

@Injectable({
    providedIn: 'root'
})
export class PersonService{
    constructor(private http: HttpClient) { }

    getPersonByColor(color: ColorType) {
        var url = apiUrl + '/quantity'
        if(color != null)
            url += '?color='+color;
        return this.http.get<QuantityPersonByColor>(url);
    }
}