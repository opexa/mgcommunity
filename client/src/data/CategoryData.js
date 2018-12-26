import Data from './Data'
const baseUrl = 'category'

class CategoryData {
  static getFeed(categoryId, page) {
    return Data.get(`${baseUrl}/feed/${categoryId}?page=${page}`, true)
  }
}

export default CategoryData
