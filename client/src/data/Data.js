import Auth from '../components/account/Auth'
const baseUrl = 'http://localhost:11185/api/'

class Data {
  static get(url, authenticated) {
    let options = getOptions('GET')
    applyAuthorizationHeader(options, authenticated)
    
    return window.fetch(`${baseUrl}${url}`, options)
      .then(handleJsonResponse)
      .catch(handleServerError)
  }

  static post(url, data, authenticated) {
    let options = getOptions('POST', data)
    applyAuthorizationHeader(options, authenticated)

    return window.fetch(`${baseUrl}${url}`, options)
      .then(handleJsonResponse)
      .catch(handleServerError)
  }

  static put(url, data, authenticated) {
    let options = getOptions('PUT', data)
    applyAuthorizationHeader(options, authenticated)

    return window.fetch(`${baseUrl}${url}`, options)
      .then(handleJsonResponse)
      .catch(handleServerError)
  }
}

const getOptions = (method, data) => {
  let options = {
    method: method,
    mode: 'cors',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/x-www-form-urlencoded'
    }
  }

  if (method === 'POST' || method === 'PUT') {
    let reqBody = []
    for (let prop in data) {
      let encodedKey = encodeURIComponent(prop)
      let encodedValue = encodeURIComponent(data[prop])
      reqBody.push(`${encodedKey}=${encodedValue}`)
    }
    reqBody = reqBody.join('&')
    options.body = reqBody
  }

  return options
}

const handleJsonResponse = res => res.json()

const handleServerError = err => ({
  success: false,
  error: err || 'Възникна някаква грешка. Моля опитайте отново.'
})

const applyAuthorizationHeader = (options, authenticated) => {
  if (authenticated) {
    options.headers['Authorization'] = `Bearer ${Auth.getToken()}`
  }
}

export default Data
