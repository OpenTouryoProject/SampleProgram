import * as React from 'react';
import 'isomorphic-fetch';

export class FetchData extends React.Component {
    constructor() {
        super();
        this.state = { forecasts: [], loading: true };
        fetch('http://localhost:5000/hoge1.json')
            .then(response => response.json())
            .then(data => {
            this.setState({ forecasts: data, loading: false });
        });
    }
    render() {
        let contents = this.state.loading
            ? React.createElement("p", null,
                React.createElement("em", null, "Loading..."))
            : FetchData.renderForecastsTable(this.state.forecasts);
        return React.createElement("div", null,
            React.createElement("h1", null, "Weather forecast"),
            React.createElement("p", { className: 'text-primary' }, "This component demonstrates fetching data from the server."),
            //React.createElement("p", null, JSON.stringify(this.state.forecasts)),
            contents);
    }
    static renderForecastsTable(forecasts) {
        return React.createElement("table", { className: 'table' },
            React.createElement("thead", null,
                React.createElement("tr", null,
                    React.createElement("th", null, "Date"),
                    React.createElement("th", null, "Temp. (C)"),
                    React.createElement("th", null, "Temp. (F)"),
                    React.createElement("th", null, "Summary"))),
            React.createElement("tbody", null, forecasts.map(forecast =>
                React.createElement("tr", { key: forecast.DateFormatted },
                React.createElement("td", null, forecast.DateFormatted),
                React.createElement("td", null, forecast.TemperatureC),
                React.createElement("td", null, forecast.TemperatureF),
                React.createElement("td", null, forecast.Summary)))));
    }
}