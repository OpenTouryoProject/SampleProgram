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
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);
        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            { contents }
        </div>;
    }
    static renderForecastsTable(forecasts) {
        console.log("components/FetchData.renderForecastsTable: " + JSON.stringify(forecasts));

        return <table className='table'>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
            {forecasts.map(forecast =>
                <tr key={ forecast.DateFormatted }>
                    <td>{ forecast.DateFormatted }</td>
                    <td>{ forecast.TemperatureC }</td>
                    <td>{ forecast.TemperatureF }</td>
                    <td>{ forecast.Summary }</td>
                </tr>
            )}
            </tbody>
        </table>;
    }
}