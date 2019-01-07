import Data from './Data'
const baseUrl = 'category'

class CategoryData {
  static getFeed(categoryId, page) {
    return Data.get(`${baseUrl}/feed?id=${categoryId}&page=${page}`, true)
  }

  static getName(id) {
    return Data.get(`${baseUrl}/name?id=${id}`, true)
  }
}

export default CategoryData
