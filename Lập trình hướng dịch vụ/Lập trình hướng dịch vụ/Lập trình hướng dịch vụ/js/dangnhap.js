(function () {
  'use strict';

  var app = angular.module('UngDungDangNhap', []);

  // --- CẤU HÌNH API ---
  app.constant('API_URL', 'http://localhost:5000/api/TaiKhoan/login');

  // --- INTERCEPTOR (Giữ nguyên) ---
  app.factory('AuthInterceptor', function () {
    return {
      request: function (config) {
        var token = localStorage.getItem('token');
        if (token) config.headers.Authorization = 'Bearer ' + token;
        return config;
      }
    };
  });
  app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('AuthInterceptor');
  });

  // --- SERVICE ---
  app.factory('AuthService', function ($http, $window, $q, API_URL) {

    function luuThongTin(data) {
      if (data.Token) localStorage.setItem('token', data.Token);
      localStorage.setItem('user_info', JSON.stringify(data));
      localStorage.setItem('isLoggedIn', 'true');
    }

    // >>> SỬA Ở ĐÂY: SO SÁNH VỚI CHỮ "ADMIN", "GV" <<<
    // Hàm điều hướng "Bất chấp"
    function dieuHuongTheoQuyen(maLoai) {
      
      // BƯỚC 1: Xử lý dữ liệu triệt để
      // Chuyển thành chuỗi -> Viết hoa toàn bộ -> Cắt sạch khoảng trắng thừa
      var role = String(maLoai).toUpperCase().trim(); 

      // BƯỚC 2: Hiện nguyên hình xem nó là cái gì (Quan trọng để debug)
      // Dấu [] giúp bạn nhìn thấy nếu có khoảng trắng thừa
      alert("🔍 SOI DỮ LIỆU:\n- Gốc: " + maLoai + "\n- Sau khi xử lý: [" + role + "]");

      // BƯỚC 3: So sánh
      // Bây giờ thì chấp cả "admin", "Admin ", "ADMIN   "...
      if (role === "ADMIN") {
        alert("✅ Khớp lệnh ADMIN -> Vào trang Quản trị");
        $window.location.href = 'admin.html';
      } 
      else if (role === "GV" || role === "GIAOVIEN") {
        alert("✅ Khớp lệnh GV -> Vào trang Giáo viên");
        $window.location.href = 'diemdanh.html';
      } 
      else if (role === "PH") {
        alert("✅ Xin chào PHỤ HUYNH -> Vào xem Hồ sơ chi tiết");
        // Chuyển đến trang dành cho phụ huynh (bạn kiểm tra lại tên file html nhé)
        $window.location.href = 'hosochitiet.html'; 
      }
      else {
        alert("⛔ Lỗi: Quyền '" + role + "' không tồn tại trong hệ thống!");
        // Không chuyển trang hoặc quay về trang login
      }
    }

    function login(username, password) {
      if (!username || !password) return $q.reject('Vui lòng nhập đủ thông tin!');

      return $http.post(API_URL, {
        TenTaiKhoan: username,
        MatKhau: password
      })
      .then(function (res) {
        var data = res.data;
        
        // Bắt lỗi nếu server trả về null
        if (!data) throw new Error('Dữ liệu trả về rỗng!');

        // Lấy trường MaLoai (Chấp nhận cả viết hoa/thường)
        var role = data.MaLoai || data.maLoai;

        if (!role) {
            alert("Lỗi dữ liệu: Tài khoản này không có MaLoai!");
            return;
        }

        luuThongTin(data);
        dieuHuongTheoQuyen(role);

        return data;
      })
      .catch(function (err) {
        var msg = 'Đăng nhập thất bại!';
        if (err.status === 400) msg = "Sai tài khoản hoặc mật khẩu!";
        else if (err.status === 404) msg = "Sai đường dẫn API!";
        else if (err.status === -1) msg = "Không kết nối được Server!";
        throw new Error(msg);
      });
    }

    return { login: login };
  });

  // --- CONTROLLER ---
  app.controller('BoDieuKienDangNhap', function ($scope, AuthService) {
    $scope.xuLyDangNhap = function () {
      $scope.thongBaoLoi = "";
      $scope.dangTai = true;

      AuthService.login($scope.taiKhoan, $scope.matKhau)
        .catch(function (e) {
          $scope.thongBaoLoi = e.message;
          $scope.dangTai = false;
          if(!$scope.$$phase) $scope.$apply();
        });
    };
  });

})();