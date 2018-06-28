// fetchの開始前
export const GET_POSTS_REQUEST = () => {
  return {
    type: 'GET_POSTS_REQUEST'
  }
};

// fetchの成功
export const GET_POSTS_SUCCESS = (data, startDateIndex) => {
  return {
    type: 'GET_POSTS_SUCCESS',
    forecasts: data,
    startDateIndex: startDateIndex
  }
};

// fetchの失敗
export const GET_POSTS_FAILURE = (error) => {
  return {
    type: 'GET_POSTS_FAILURE',
    error: error
  }
};

// fetchのルート
export const GET_DATA_ASYNC = (startDateIndex) => {
  return (dispatch) => {

    // URL
    let url = 'http://localhost:5000/hoge' + startDateIndex + '.json';
    console.log("GET_DATA_ASYNC > url: " + url);

    // リクエスト開始前処理
    dispatch(GET_POSTS_REQUEST());

    // fetchする。
    fetch(url)　
    .then(response => response.json())
    .then(data => 
      {
        console.log("GET_DATA_ASYNC > GET_POSTS_SUCCESS: " + JSON.stringify(data));
        dispatch(GET_POSTS_SUCCESS(data, startDateIndex));
      }
    )
    .catch(
      // 異常系
      error => {
        console.log("GET_DATA_ASYNC > GET_POSTS_FAILURE: " + JSON.stringify(error));
        dispatch(GET_POSTS_FAILURE(error));
      }
    );
  }
}