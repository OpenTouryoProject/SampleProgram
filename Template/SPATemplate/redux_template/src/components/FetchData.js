import * as React from 'react';
import { Link } from 'react-router-dom';

export default class FetchData extends React.Component {
    componentWillMount() {
        // 初回実行
        console.log("this.props.match: " + JSON.stringify(this.props.match));
        let startDateIndex = parseInt(this.props.match.params.startDateIndex) || 0;
        this.props.GET_DATA(startDateIndex);
    }

    /*componentWillReceiveProps(nextProps) {
        // route paramsなど、param変更時
        console.log("nextProps.match: " + JSON.stringify(nextProps.match));
        let startDateIndex = parseInt(nextProps.match.params.startDateIndex) || 0;
        this.props.GET_DATA(startDateIndex);
    }*/

    render() {
        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
            {/*}
            { this.renderForecastsTable() }
            { this.renderPagination() }
            */} {this.props.counter}
        </div>;
    }
    static renderForecastsTable(forecasts) {
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

    static renderPagination() {
        let prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
        let nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={ `/fetchdata/${ prevStartDateIndex }` }>Previous</Link>
            <Link className='btn btn-default pull-right' to={ `/fetchdata/${ nextStartDateIndex }` }>Next</Link>
            { this.props.isLoading ? <span>Loading...</span> : [] }
        </p>;
    }
}