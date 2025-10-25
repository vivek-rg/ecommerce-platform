package com.ecommerce.project.service;

import com.ecommerce.project.exceptions.APIException;
import com.ecommerce.project.exceptions.ResourceNotFoundException;
import com.ecommerce.project.model.Category;
import com.ecommerce.project.model.Product;
import com.ecommerce.project.payload.ProductDTO;
import com.ecommerce.project.payload.ProductResponse;
import com.ecommerce.project.repositories.CategoryRepository;
import com.ecommerce.project.repositories.ProductRepository;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.List;

@Service
public class ProductServiceImpl implements ProductService {

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private CategoryRepository categoryRepository;

    @Autowired
    private ModelMapper modelMapper;

    @Autowired
    private FileService fileService;

    @Value("${project.image}")
    private String path;

    @Override
    public ProductDTO addProduct(Long categoryId, ProductDTO productDTO) {
        Category category= categoryRepository.findById(categoryId)
                .orElseThrow(()->
                new ResourceNotFoundException("Category","catgoryId",categoryId));

        boolean ifProductNotPresent =true;
        List<Product> products=category.getProducts();
        for (int i = 0; i < products.size(); i++) {
            if(products.get(i).getProductName().equals(productDTO.getProductName())){
                ifProductNotPresent=false;
                break;
            }
        }

       if(ifProductNotPresent){
           Product product= modelMapper.map(productDTO,Product.class);
           product.setImage("default.png");
           product.setCategory(category);
           double specialPrice= product.getPrice()-((product.getDiscount() * 0.01)*product.getPrice());
           product.setSpecialPrice(specialPrice);
           Product savedProduct = productRepository.save(product);
           return modelMapper.map(savedProduct, ProductDTO.class);

       }
       else
       {
           throw new APIException("product already exists!!");
       }
    }

    @Override
    public ProductResponse getAllProducts(Integer pageNumber, Integer pageSize, String sortBy, String sortOrder) {

       Sort sortByAndOrder=sortOrder.equalsIgnoreCase("asc")
               ?Sort.by(sortBy).ascending()
               : Sort.by(sortBy).descending();

        Pageable pageDetails= PageRequest.of(pageNumber,pageSize,sortByAndOrder);
        Page<Product> pageProduct= productRepository.findAll(pageDetails);



        List<Product> products = pageProduct.getContent();
        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();

        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
        productResponse.setPageNumber(pageProduct.getNumber());
        productResponse.setPageSize(pageProduct.getSize());
        productResponse.setTotalPages(pageProduct.getTotalPages());
        productResponse.setTotalElements(pageProduct.getTotalElements());
        productResponse.setLastPage(pageProduct.isLast());
        return productResponse;
    }

    @Override
    public ProductResponse searchByCategory(Long categoryId, Integer pageNumber, Integer pageSize, String sortBy, String sortOrder) {

        Category category= categoryRepository.findById(categoryId)
                .orElseThrow(()->
                        new ResourceNotFoundException("Category","catgoryId",categoryId));


        Sort sortByAndOrder=sortOrder.equalsIgnoreCase("asc")
                ?Sort.by(sortBy).ascending()
                : Sort.by(sortBy).descending();

        Pageable pageDetails= PageRequest.of(pageNumber,pageSize,sortByAndOrder);
        Page<Product> pageProduct= productRepository.findByCategoryOrderByPriceAsc(category,pageDetails);



        List<Product> products = pageProduct.getContent();
        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();

        if(products.size()==0)
        {
            throw new APIException(category.getCategoryName()+" category does not have any product");
        }
        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
        productResponse.setPageNumber(pageProduct.getNumber());
        productResponse.setPageSize(pageProduct.getSize());
        productResponse.setTotalPages(pageProduct.getTotalPages());
        productResponse.setTotalElements(pageProduct.getTotalElements());
        productResponse.setLastPage(pageProduct.isLast());

        return productResponse;
    }

    @Override
    public ProductResponse searchByKeyword(String keyword, Integer pageNumber, Integer pageSize, String sortBy, String sortOrder) {

        Sort sortByAndOrder=sortOrder.equalsIgnoreCase("asc")
                ?Sort.by(sortBy).ascending()
                : Sort.by(sortBy).descending();

        Pageable pageDetails= PageRequest.of(pageNumber,pageSize,sortByAndOrder);
        Page<Product> pageProduct= productRepository.findByProductNameLikeIgnoreCase('%'+keyword+'%',pageDetails);



        List<Product> products = pageProduct.getContent();


        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();

        if(products.size()==0)
        {
            throw new APIException("product not found with keyword "+keyword);
        }

        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
        productResponse.setPageNumber(pageProduct.getNumber());
        productResponse.setPageSize(pageProduct.getSize());
        productResponse.setTotalPages(pageProduct.getTotalPages());
        productResponse.setTotalElements(pageProduct.getTotalElements());
        productResponse.setLastPage(pageProduct.isLast());
        return productResponse;
    }

    @Override
    public ProductDTO updateProduct(Long productId, ProductDTO productDTO) {
        Product productFromDB = productRepository.findById(productId)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "productId", productId));
        Product product= modelMapper.map(productDTO,Product.class);

             productFromDB.setProductName(product.getProductName());
             productFromDB.setDescription(product.getDescription());
            productFromDB.setPrice(product.getPrice());
            productFromDB.setDiscount(product.getDiscount());
            productFromDB.setQuantity(product.getQuantity());
            productFromDB.setSpecialPrice(product.getPrice()-((product.getDiscount() * 0.01)*product.getPrice()));

            productRepository.save(productFromDB);

        ProductDTO updatedproductDTO= modelMapper.map(productFromDB,ProductDTO.class);
        return updatedproductDTO;
    }

    @Override
    public ProductDTO deleteProduct(Long productId) {
        Product productFromDB = productRepository.findById(productId)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "productId", productId));
        productRepository.delete(productFromDB);
        ProductDTO productDTO= modelMapper.map(productFromDB,ProductDTO.class);

        return productDTO;
    }

    @Override
    public ProductDTO updateProductImage(Long productId, MultipartFile image) throws IOException {
        //get the product from the DB
        Product productFromDb= productRepository.findById(productId)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "productId", productId));

        //Upload the image to the server
        //get the file name of the uploaded image
//        String path= "images/";
        String fileName= fileService.uploadImage(path,image);


        //updating new file name to the product
        productFromDb.setImage(fileName);

        //save the updated product
        productRepository.save(productFromDb);

        //return DTO after mapping
        return modelMapper.map(productFromDb,ProductDTO.class);
    }


}
