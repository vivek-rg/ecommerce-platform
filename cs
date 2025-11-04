[33mcommit 9dda6214269e98e201496f58e1f4eb931bc3f80d[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mmain[m[33m)[m
Author: VIVEK R GOWDA <vivekvicky1204@gmail.com>
Date:   Tue Nov 4 23:29:24 2025 +0530

    nov........................4

[1mdiff --git a/pom.xml b/pom.xml[m
[1mindex e52860b..63e978f 100644[m
[1m--- a/pom.xml[m
[1m+++ b/pom.xml[m
[36m@@ -46,9 +46,21 @@[m
             <artifactId>spring-boot-starter-data-jpa</artifactId>[m
         </dependency>[m
 [m
[32m+[m[32m<!--        <dependency>-->[m
[32m+[m[32m<!--            <groupId>com.h2database</groupId>-->[m
[32m+[m[32m<!--            <artifactId>h2</artifactId>-->[m
[32m+[m[32m<!--            <scope>runtime</scope>-->[m
[32m+[m[32m<!--        </dependency>-->[m
[32m+[m
[32m+[m[32m<!--        <dependency>-->[m
[32m+[m[32m<!--            <groupId>com.mysql</groupId>-->[m
[32m+[m[32m<!--            <artifactId>mysql-connector-j</artifactId>-->[m
[32m+[m[32m<!--            <scope>runtime</scope>-->[m
[32m+[m[32m<!--        </dependency>-->[m
[32m+[m
         <dependency>[m
[31m-            <groupId>com.h2database</groupId>[m
[31m-            <artifactId>h2</artifactId>[m
[32m+[m[32m            <groupId>org.postgresql</groupId>[m
[32m+[m[32m            <artifactId>postgresql</artifactId>[m
             <scope>runtime</scope>[m
         </dependency>[m
 [m
[1mdiff --git a/src/main/java/com/ecommerce/project/VivEcomApplication.java b/src/main/java/com/ecommerce/project/VivEcomApplication.java[m
[1mdeleted file mode 100644[m
[1mindex 361077b..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/VivEcomApplication.java[m
[1m+++ /dev/null[m
[36m@@ -1,13 +0,0 @@[m
[31m-package com.ecommerce.project;[m
[31m-[m
[31m-import org.springframework.boot.SpringApplication;[m
[31m-import org.springframework.boot.autoconfigure.SpringBootApplication;[m
[31m-[m
[31m-@SpringBootApplication[m
[31m-public class VivEcomApplication {[m
[31m-[m
[31m-    public static void main(String[] args) {[m
[31m-        SpringApplication.run(VivEcomApplication.class, args);[m
[31m-    }[m
[31m-[m
[31m-}[m
[1mdiff --git a/src/main/java/com/ecommerce/project/config/AppConfig.java b/src/main/java/com/ecommerce/project/config/AppConfig.java[m
[1mdeleted file mode 100644[m
[1mindex f8aa44e..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/config/AppConfig.java[m
[1m+++ /dev/null[m
[36m@@ -1,14 +0,0 @@[m
[31m-package com.ecommerce.project.config;[m
[31m-[m
[31m-import org.modelmapper.ModelMapper;[m
[31m-import org.springframework.context.annotation.Bean;[m
[31m-import org.springframework.context.annotation.Configuration;[m
[31m-[m
[31m-@Configuration[m
[31m-public class AppConfig {[m
[31m-[m
[31m-    @Bean[m
[31m-    public ModelMapper modelMapper(){[m
[31m-        return new ModelMapper();[m
[31m-    }[m
[31m-}[m
[1mdiff --git a/src/main/java/com/ecommerce/project/config/AppConstants.java b/src/main/java/com/ecommerce/project/config/AppConstants.java[m
[1mdeleted file mode 100644[m
[1mindex e41221a..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/config/AppConstants.java[m
[1m+++ /dev/null[m
[36m@@ -1,11 +0,0 @@[m
[31m-package com.ecommerce.project.config;[m
[31m-[m
[31m-public class AppConstants {[m
[31m-    public static final String PAGE_NUMBER = "0";[m
[31m-    public static final String PAGE_SIZE = "50";[m
[31m-    public static final String SORT_CATEGORIES_BY = "categoryId";[m
[31m-    public static final String SORT_DIR = "asc";[m
[31m-[m
[31m-    public static final String SORT_PRODUCTS_BY = "productId";[m
[31m-}[m
[31m-[m
[1mdiff --git a/src/main/java/com/ecommerce/project/controller/AddressController.java b/src/main/java/com/ecommerce/project/controller/AddressController.java[m
[1mdeleted file mode 100644[m
[1mindex e69de29..0000000[m
[1mdiff --git a/src/main/java/com/ecommerce/project/controller/AuthController.java b/src/main/java/com/ecommerce/project/controller/AuthController.java[m
[1mdeleted file mode 100644[m
[1mindex 8d01866..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/controller/AuthController.java[m
[1m+++ /dev/null[m
[36m@@ -1,168 +0,0 @@[m
[31m-package com.ecommerce.project.controller;[m
[31m-[m
[31m-import com.ecommerce.project.model.AppRole;[m
[31m-import com.ecommerce.project.model.Role;[m
[31m-import com.ecommerce.project.model.User;[m
[31m-import com.ecommerce.project.repositories.RoleRepository;[m
[31m-import com.ecommerce.project.repositories.UserRepository;[m
[31m-import com.ecommerce.project.security.jwt.JwtUtils;[m
[31m-import com.ecommerce.project.security.request.LoginRequest;[m
[31m-import com.ecommerce.project.security.request.SignupRequest;[m
[31m-import com.ecommerce.project.security.response.MessageResponse;[m
[31m-import com.ecommerce.project.security.response.UserInfoResponse;[m
[31m-import com.ecommerce.project.security.services.UserDetailsImpl;[m
[31m-import jakarta.validation.Valid;[m
[31m-import org.springframework.beans.factory.annotation.Autowired;[m
[31m-import org.springframework.http.HttpHeaders;[m
[31m-import org.springframework.http.HttpStatus;[m
[31m-import org.springframework.http.ResponseCookie;[m
[31m-import org.springframework.http.ResponseEntity;[m
[31m-import org.springframework.security.authentication.AuthenticationManager;[m
[31m-import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;[m
[31m-import org.springframework.security.core.Authentication;[m
[31m-import org.springframework.security.core.AuthenticationException;[m
[31m-import org.springframework.security.core.context.SecurityContextHolder;[m
[31m-[m
[31m-import org.springframework.security.crypto.password.PasswordEncoder;[m
[31m-import org.springframework.web.bind.annotation.*;[m
[31m-[m
[31m-import java.util.*;[m
[31m-[m
[31m-@RestController[m
[31m-@RequestMapping("/api/auth")[m
[31m-public class AuthController {[m
[31m-[m
[31m-    @Autowired[m
[31m-    private JwtUtils jwtUtils;[m
[31m-[m
[31m-    @Autowired[m
[31m-    private AuthenticationManager authenticationManager;[m
[31m-[m
[31m-    @Autowired[m
[31m-    UserRepository userRepository;[m
[31m-[m
[31m-    @Autowired[m
[31m-    RoleRepository roleRepository;[m
[31m-[m
[31m-    @Autowired[m
[31m-    PasswordEncoder encoder;[m
[31m-[m
[31m-[m
[31m-    @PostMapping("/signin")[m
[31m-    public ResponseEntity<?> authenticateUser(@RequestBody LoginRequest loginRequest)[m
[31m-    {[m
[31m-        Authentication authentication;[m
[31m-        try{[m
[31m-            authentication = authenticationManager.authenticate([m
[31m-                    new UsernamePasswordAuthenticationToken(loginRequest.getUsername(),[m
[31m-                            loginRequest.getPassword())[m
[31m-            ) ;[m
[31m-        }catch (AuthenticationException exception){[m
[31m-            Map<String,Object> map = new HashMap<>();[m
[31m-            map.put("message","Bad Credentials");[m
[31m-            map.put("status",false);[m
[31m-[m
[31m-            return new ResponseEntity<Object>(map, HttpStatus.NOT_FOUND);[m
[31m-[m
[31m-        }[m
[31m-        SecurityContextHolder.getContext().setAuthentication(authentication);[m
[31m-        UserDetailsImpl userDetails = (UserDetailsImpl) authentication.getPrincipal();[m
[31m-        ResponseCookie jwtCookie=jwtUtils.generateJwtCookie(userDetails);[m
[31m-        List<String> roles=userDetails.getAuthorities().stream().map(item->item.getAuthority()).toList();[m
[31m-[m
[31m-        UserInfoResponse response=new UserInfoResponse(userDetails.getId(),jwtCookie.toString(),roles,userDetails.getUsername());[m
[31m-        return ResponseEntity.ok().header(HttpHeaders.SET_COOKIE, jwtCookie.toString()).body(response);[m
[31m-    }[m
[31m-[m
[31m-    @PostMapping("/signup")[m
[31m-    public ResponseEntity<?> registerUser(@Valid @RequestBody SignupRequest signupRequest){[m
[31m-[m
[31m-[m
[31m-        if(userRepository.existsByUserName(signupRequest.getUsername()))[m
[31m-        {[m
[31m-            return  ResponseEntity.badRequest()[m
[31m-                    .body(new MessageResponse("Error : Username is already taken!"));[m
[31m-[m
[31m-        }[m
[31m-[m
[31m-        if(userRepository.existsByEmail(signupRequest.getEmail()))[m
[31m-        {[m
[31m-            return  ResponseEntity.badRequest()[m
[31m-                    .body(new MessageResponse("Error : Email is already taken!"));[m
[31m-[m
[31m-        }[m
[31m-[m
[31m-        User user=new User([m
[31m-                signupRequest.getUsername(),[m
[31m-                signupRequest.getEmail(),[m
[31m-                encoder.encode(signupRequest.getPassword())[m
[31m-        );[m
[31m-        Set<String> strRoles=signupRequest.getRole();[m
[31m-        Set<Role> roles=new HashSet<>();[m
[31m-[m
[31m-        if(strRoles==null)[m
[31m-        {[m
[31m-            Role userRole=roleRepository.findByRoleName(AppRole.ROLE_USER)[m
[31m-                    .orElseThrow(()-> new RuntimeException("Error : role is not found"));[m
[31m-            roles.add(userRole);[m
[31m-        }[m
[31m-        else {[m
[31m-            strRoles.forEach(role->{[m
[31m-                switch (role) {[m
[31m-                    case "admin":[m
[31m-                        Role adminRole=roleRepository.findByRoleName(AppRole.ROLE_ADMIN)[m
[31m-                            .orElseThrow(()-> new RuntimeException("Error : role is not found"));[m
[31m-                        roles.add(adminRole);[m
[31m-                        break;[m
[31m-[m
[31m-                    case "seller" :[m
[31m-                        Role sellerRole=roleRepository.findByRoleName(AppRole.ROLE_SELLER)[m
[31m-                                .orElseThrow(()-> new RuntimeException("Error : role is not found"));[m
[31m-                            roles.add(sellerRole);[m
[31m-                            break;[m
[31m-[m
[31m-[m
[31m-                    default:[m
[31m-                        Role userRole=roleRepository.findByRoleName(AppRole.ROLE_USER)[m
[31m-                                .orElseThrow(()-> new RuntimeException("Error : role is not found"));[m
[31m-                        roles.add(userRole);[m
[31m-                                break;[m
[31m-                }[m
[31m-            });[m
[31m-        }[m
[31m-        user.setRoles(roles);[m
[31m-        userRepository.save(user);[m
[31m-[m
[31m-        return  ResponseEntity.ok(new MessageResponse("User registered successfully!"));[m
[31m-    }[m
[31m-[m
[31m-    @GetMapping("/username")[m
[31m-    public String currentUserName(Authentication authentication){[m
[31m-        if(authentication!=null)[m
[31m-        {[m
[31m-            return authentication.getName();[m
[31m-        }[m
[31m-        else[m
[31m-            {[m
[31m-            return "";[m
[31m-            }[m
[31m-    }[m
[31m-    @GetMapping("/user")[m
[31m-    public ResponseEntity<UserInfoResponse> getUserDetails(Authentication authentication){[m
[31m-        UserDetailsImpl userDetails = (UserDetailsImpl) authentication.getPrincipal();[m
[31m-        List<String> roles=userDetails.getAuthorities().stream()[m
[31m-                .map(item->item.getAuthority()).toList();[m
[31m-[m
[31m-        UserInfoResponse response=new UserInfoResponse(userDetails.getId(),roles,userDetails.getUsername());[m
[31m-        return ResponseEntity.ok().body(response);[m
[31m-[m
[31m-    }[m
[31m-[m
[31m-    @PostMapping("/signout")[m
[31m-    public ResponseEntity<?> signout(){[m
[31m-        ResponseCookie cookie=jwtUtils.getCleanJwtCookie();[m
[31m-        return ResponseEntity.ok()[m
[31m-                .header(HttpHeaders.SET_COOKIE, cookie.toString()).body(new  MessageResponse("You've been signed out!"));[m
[31m-[m
[31m-    }[m
[31m-}[m
[1mdiff --git a/src/main/java/com/ecommerce/project/controller/CartController.java b/src/main/java/com/ecommerce/project/controller/CartController.java[m
[1mdeleted file mode 100644[m
[1mindex e69de29..0000000[m
[1mdiff --git a/src/main/java/com/ecommerce/project/controller/CategoryController.java b/src/main/java/com/ecommerce/project/controller/CategoryController.java[m
[1mdeleted file mode 100644[m
[1mindex 01b6e36..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/controller/CategoryController.java[m
[1m+++ /dev/null[m
[36m@@ -1,63 +0,0 @@[m
[31m-package com.ecommerce.project.controller;[m
[31m-[m
[31m-[m
[31m-import com.ecommerce.project.config.AppConstants;[m
[31m-import com.ecommerce.project.model.Category;[m
[31m-import com.ecommerce.project.payload.CategoryResponse;[m
[31m-import com.ecommerce.project.payload.CategoryDTO;[m
[31m-import com.ecommerce.project.service.CategoryService;[m
[31m-import jakarta.validation.Valid;[m
[31m-import org.modelmapper.ModelMapper;[m
[31m-import org.springframework.beans.factory.annotation.Autowired;[m
[31m-import org.springframework.http.HttpStatus;[m
[31m-import org.springframework.http.ResponseEntity;[m
[31m-import org.springframework.web.bind.annotation.*;[m
[31m-[m
[31m-@RestController[m
[31m-@RequestMapping("/api")[m
[31m-public class CategoryController {[m
[31m-[m
[31m-[m
[31m-    @Autowired[m
[31m-    private CategoryService categoryService;[m
[31m-    @Autowired[m
[31m-    private ModelMapper modelMapper;[m
[31m-[m
[31m-[m
[31m-[m
[31m-    @GetMapping("/public/categories")[m
[31m-    public ResponseEntity<CategoryResponse> getCategories([m
[31m-            @RequestParam(name = "pageNumber",defaultValue = AppConstants.PAGE_NUMBER,required = false) Integer pageNumber,[m
[31m-            @RequestParam(name = "pageSize",defaultValue = AppConstants.PAGE_SIZE,required = false) Integer pageSize,[m
[31m-            @RequestParam(name = "sortBy",defaultValue = AppConstants.SORT_CATEGORIES_BY,required = false) String sortBy,[m
[31m-            @RequestParam(name = "sortOrder",defaultValue = AppConstants.SORT_DIR,required = false) String sortOrder) {[m
[31m-        CategoryResponse categoryResponse = categoryService.getAllCategories(pageNumber, pageSize,sortBy,sortOrder);[m
[31m-        return new ResponseEntity<>(categoryResponse,HttpStatus.OK);[m
[31m-    }[m
[31m-[m
[31m-    @PostMapping("/public/categories")[m
[31m-    public ResponseEntity<CategoryDTO> addCategory(@Valid @RequestBody CategoryDTO categoryDTO) {[m
[31m-        CategoryDTO savedCategoryDTO=categoryService.createCategory(categoryDTO);[m
[31m-        return new ResponseEntity<>(savedCategoryDTO,HttpStatus.CREATED);[m
[31m-    }[m
[31m-[m
[31m-    @DeleteMapping("/public/categories/{categoryId}")[m
[31m-    public ResponseEntity<CategoryDTO> deleteCategory(@PathVariable Long categoryId) {[m
[31m-[m
[31m-            CategoryDTO deletedCategory=categoryService.deleteCategory(categoryId);[m
[31m-            return new ResponseEntity<>(deletedCategory,HttpStatus.OK);[m
[31m-[m
[31m-    }[m
[31m-[m
[31m-    @PutMapping("/public/categories/{categoryId}")[m
[31m-    public ResponseEntity<CategoryDTO> updateCategory(@Valid @RequestBody CategoryDTO categoryDTO,[m
[31m-                                                 @PathVariable Long categoryId) {[m
[31m-[m
[31m-            CategoryDTO savedCategoryDTO =categoryService.updateCategory(categoryDTO,categoryId);[m
[31m-            return new ResponseEntity<>(savedCategoryDTO,HttpStatus.OK);[m
[31m-[m
[31m-[m
[31m-    }[m
[31m-[m
[31m-[m
[31m-}[m
[1mdiff --git a/src/main/java/com/ecommerce/project/controller/ProductController.java b/src/main/java/com/ecommerce/project/controller/ProductController.java[m
[1mdeleted file mode 100644[m
[1mindex 04fb6bf..0000000[m
[1m--- a/src/main/java/com/ecommerce/project/controller/ProductController.java[m
[1m+++ /dev/null[m
[36m@@ -1,84 +0,0 @@[m
[31m-package com.ecommerce.project.controller;[m
[31m-[m
[31m-[m
[31m-import com.ecommerce.project.config.AppConstants;[m
[31m-import com.ecommerce.project.payload.ProductDTO;[m
[31m-import com.ecommerce.project.payload.ProductResponse;[m
[31m-import com.ecommerce.project.service.ProductService;[m
[31m-import jakarta.validation.Valid;[m
[31m-import org.springframework.beans.factory.annotation.Autowired;[m
[31m-import org.springframework.http.HttpStatus;[m
[31m-import org.springframework.http.ResponseEntity;[m
[31m-import org.springframework.web.bind.annotation.*;[m
[31m-import org.springframework.web.multipart.MultipartFile;[m
[31m-[m
[31m-import java.io.IOException;[m
[31m-[m
[31m-@RestController[m
[31m-@RequestMapping("/api")[m
[31m-public class ProductController {[m
[31m-[m
[31m-    @Autowired[m
[31m-    private ProductService productService;[m
[31m-[m
[31m-    @PostMapping("/admin/categories/{categoryId}/product")[m
[31m-    public ResponseEntity<ProductDTO> addProduct(@Valid @RequestBody ProductDTO productDTO,[m
[31m-                                                 @PathVariable Long categoryId){[m
[31m-       ProductDTO savedproductDTO= productService.addProduct(categoryId,productDTO);[m
[31m-       return new ResponseEntity<>(savedproductDTO,HttpStatus.CREATED);[m
[31m-    }[m
[31m-[m
[31m-    @GetMapping("/public/products")[m
[31m-    public ResponseEntity<ProductResponse> getAllProducts([m
[31m-            @RequestParam(name = "pageNumber",defaultValue = AppConstants.PAGE_NUMBER,required = false) Integer pageNumber,[m
[31m-            @RequestParam(name = "pageSize",defaultValue = AppConstants.PAGE_SIZE,required = false)  Integer pageSize  ,[m
[31m-            @RequestParam(name = "SortBy",defaultValue = AppConstants.SORT_PRODUCTS_BY,required = false)  String sortBy,[m
[31m-            @RequestParam(name = "SortOrder",defaultValue = AppConstants.SORT_DIR,required = false) String sortOrder[m
[31m-    ){[m
[31m-       ProductResponse productResponse= productService.getAllProducts(pageNumber,pageSize,sortBy,sortOrder);[m
[31m-       return new ResponseEntity<>(productResponse,HttpStatus.OK);[m
[31m-    }[m
[31m-[m
[31m-    @GetMapping("/public/categories/{categoryId}/products")[m
[31m-    public ResponseEntity<ProductResponse> getProductsByCategory(@PathVariable Long categoryId,[m
[31m-                                                                 @RequestParam(name = "pageNumber",defaultValue = AppConstants.PAGE_NUMBER,required = false) Integer pageNumber,[m
[31m-                                                                 @RequestParam(name = "pageSize",defaultValue = AppConstants.PAGE_SIZE,required = false)  Integer pageSize  ,[m
[31m-                                                                 @RequestParam(name = "SortBy",defaultValue = AppConstants.SORT_PRODUCTS_BY,required = false)  String sortBy,[m
[31m-                                                                 @RequestParam(name = "SortOrder",defaultValue = AppConstants.SORT_DIR,required = false) String sortOrder[m
[31m-    ){[m
[31m-       ProductResponse productResponse= productService.searchByCategory(categoryId,pageNumber,pageSize,sortBy,sortOrder);[m
[31m-       return new ResponseEntity<>(productResponse,HttpStatus.OK);[m
[31m-    }[m
[31m-[m
[31m-    @GetMapping("/public/products/keyword/{keyword}")[m
[31m-    public ResponseEntity<ProductResponse> getProductsByKeyword(@PathVariable String keyword,[m
[31m-                                                                @RequestParam(name = "pageNumber",defaultValue = AppConstants.PAGE_NUMBER,required = false) Integer pageNumber,[m
[31m-                                                                @RequestParam(name = "pageSize",defaultValue = AppConstants.PAGE_SIZE,required = false)  Integer pageSize  ,[m
[31m-                                                                @RequestParam(name = "SortBy",defaultValue = AppConstants.SORT_PRODUCTS_BY,required = false)  String sortBy,[m
[31m-                                                                @RequestParam(name = "SortOrder",defaultValue = AppConstant