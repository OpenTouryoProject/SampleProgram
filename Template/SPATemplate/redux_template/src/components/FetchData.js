import * as React from 'react';
import { Link } from 'react-router-dom';

export default class FetchData extends React.Component {

    componentWillMount() {
        // 初回実行
        console.log("this.props.match: " + JSON.stringify(this.props.match));
        let startDateIndex = parseInt(this.props.match.params.startDateIndex) || 1;
        this.props.GET_DATA_ASYNC(startDateIndex);
    }

    componentWillReceiveProps(nextProps) {
        // route paramsなど、param変更時
        console.log("nextProps.match: " + JSON.stringify(nextProps.match));
        let startDateIndex = parseInt(nextProps.match.params.startDateIndex) || 1;

        // 無限ループ（stack overflow）防止
        if(startDateIndex != nextProps.startDateIndex) {
            this.props.GET_DATA_ASYNC(startDateIndex);
        }
    }

    render() {
        return <div>
            <h1>Weather forecast</h1>
            <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
            { FetchData.renderForecastsTable(this.props.forecasts) }
            { FetchData.renderPagination(this.props.startDateIndex) }
        </div>;
    }

    static renderForecastsTable(forecasts) {
        console.log("components/FetchData.renderForecastsTable: " + JSON.stringify(forecasts));

        if(forecasts){
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
        else
        {
            return <p>hoge</p>;
        }
    }

    static renderPagination(startDateIndex) {
        let prevStartDateIndex = startDateIndex - 1;
        let nextStartDateIndex = startDateIndex + 1;

        if(startDateIndex)
        {
            return <p className='clearfix text-center'>
                <Link className='btn btn-default pull-left' to={ `/fetchdata/${ prevStartDateIndex }` }>Previous</Link>
                <Link className='btn btn-default pull-right' to={ `/fetchdata/${ nextStartDateIndex }` }>Next</Link>
                {/* this.props.isLoading ? <span>Loading...</span> : [] */}
            </p>;
        }
        else
        {
            return <p>hoge</p>;
        }
    }
}