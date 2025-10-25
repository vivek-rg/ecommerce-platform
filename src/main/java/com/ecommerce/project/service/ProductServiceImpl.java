package com.ecommerce.project.service;

import com.ecommerce.project.exceptions.ResourceNotFoundException;
import com.ecommerce.project.model.Category;
import com.ecommerce.project.model.Product;
import com.ecommerce.project.payload.ProductDTO;
import com.ecommerce.project.payload.ProductResponse;
import com.ecommerce.project.repositories.CategoryRepository;
import com.ecommerce.project.repositories.ProductRepository;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.multipart.MultipartFile;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;
import java.util.UUID;

@Service
public class ProductServiceImpl implements ProductService {

    @Autowired
    private ProductRepository productRepository;

    @Autowired
    private CategoryRepository categoryRepository;

    @Autowired
    private ModelMapper modelMapper;

    @Override
    public ProductDTO addProduct(Long categoryId, ProductDTO productDTO) {
        Category category= categoryRepository.findById(categoryId)
                .orElseThrow(()->
                new ResourceNotFoundException("Category","catgoryId",categoryId));

        Product product= modelMapper.map(productDTO,Product.class);
        product.setImage("default.png");
        product.setCategory(category);
        double specialPrice= product.getPrice()-((product.getDiscount() * 0.01)*product.getPrice());
        product.setSpecialPrice(specialPrice);
        Product savedProduct = productRepository.save(product);
        return modelMapper.map(savedProduct, ProductDTO.class);
    }

    @Override
    public ProductResponse getAllProducts() {
        List<Product> products = productRepository.findAll();
        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();
        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
        return productResponse;
    }

    @Override
    public ProductResponse searchByCategory(Long categoryId) {

        Category category= categoryRepository.findById(categoryId)
                .orElseThrow(()->
                        new ResourceNotFoundException("Category","catgoryId",categoryId));

        List<Product> products = productRepository.findByCategoryOrderByPriceAsc(category);
        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();
        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
        return productResponse;
    }

    @Override
    public ProductResponse searchByKeyword(String keyword) {


        List<Product> products = productRepository.findByProductNameLikeIgnoreCase('%'+keyword+'%');
        List<ProductDTO> productDTOS=products.stream()
                .map(product -> modelMapper.map(product,ProductDTO.class)).toList();
        ProductResponse productResponse= new ProductResponse();
        productResponse.setContent(productDTOS);
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
        String path= "images/";
        String fileName= uploadImage(path,image);


        //updating new file name to the product
        productFromDb.setImage(fileName);

        //save the updated product
        productRepository.save(productFromDb);

        //return DTO after mapping
        return modelMapper.map(productFromDb,ProductDTO.class);
    }

    private String uploadImage(String path, MultipartFile file) throws IOException {

        //get the file name of current/original file
        String OriginalFileName=file.getOriginalFilename();

        //generate a unique file name
        String randomId= UUID.randomUUID().toString();
        String fileName=randomId.concat(OriginalFileName.substring(OriginalFileName.lastIndexOf('.')));

        String filePath=path+ File.separator+fileName;

        //check if path exist and create
        File folder=new File(path);
        if(!folder.exists()){
            folder.mkdirs();
        }

        //upload to server
        Files.copy(file.getInputStream(), Paths.get(filePath));

        //return the file name
        return fileName;

    }
}
