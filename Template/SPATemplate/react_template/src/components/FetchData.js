import * as React from 'react';
import 'isomorphic-fetch';
export class FetchData extends React.Component {
    constructor() {
        super();
        this.state = { forecasts: [], loading: true };
        fetch('api/SampleData/WeatherForecasts')
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
            React.createElement("p", null, "This component demonstrates fetching data from the server."),
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
            React.createElement("tbody", null, forecasts.map(forecast => React.createElement("tr", { key: forecast.dateFormatted },
                React.createElement("td", null, forecast.dateFormatted),
                React.createElement("td", null, forecast.temperatureC),
                React.createElement("td", null, forecast.temperatureF),
                React.createElement("td", null, forecast.summary)))));
    }
}