import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";

export class SignalRService {
    private hubConnection: signalR.HubConnection;

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
                                .withUrl('http://localhost:20000/person-events')
                                .build();
        this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }
    public addPersonCommandListener = (getData: Function) => {
        this.hubConnection.on('personCommandExecuted', (data) => {
            getData();
        });
    }
}