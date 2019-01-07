const QueryParser = {
  parseSearch: search => {
    let returnObj = {}
    let paramString = search.replace('?', '').split('&')
    for (let param in paramString) {
      let keyValue = paramString[param].split('=')
      returnObj[keyValue[0]] = keyValue[1]
    }
    return returnObj
  }
}

export default QueryParser