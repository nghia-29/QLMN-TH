var app = angular.module('UngDungQuanLy', []);

app.controller('HocPhiController', function($scope, $http) {
    
    // --- CẤU HÌNH API ---
    var currentUrl = "http://localhost:6001"; 
    var apiBase = currentUrl + "/api/HocPhi"; // Đổi thành HocPhi

    $scope.dsHocPhi = [];
    $scope.hp = {};
    $scope.cheDoSua = false;

    // 1. LOAD DANH SÁCH
    $scope.layDanhSach = function() {
        $http.get(apiBase + "/get-all")
        .then(function(res) {
            $scope.dsHocPhi = res.data;
        }, function(err) {
            console.error(err);
        });
    };
    $scope.layDanhSach();

    // 2. CHỌN DÒNG
    $scope.chonDong = function(item) {
        // Map dữ liệu từ chữ thường (trên bảng) sang chữ Hoa (để gửi API)
        $scope.hp = {
            MaHocPhi:      item.maHocPhi,
            MaHocSinh:     item.maHocSinh,
            ThangThu:      item.thangThu,
            TongTien:      item.tongTien,
            TrangThai:     item.trangThai,
            HinhThuc:      item.hinhThuc
        };

        // Xử lý Ngày Thanh Toán (Có giờ phút)
        if (item.ngayThanhToan) {
            $scope.hp.NgayThanhToan = new Date(item.ngayThanhToan);
        }
        
        $scope.cheDoSua = true;
    };

    // 3. THÊM MỚI (TẠO PHIẾU THU)
    // 3. THÊM MỚI (Đã nâng cấp)
    $scope.themMoi = function() {
        // 1. Validate dữ liệu quan trọng
        if (!$scope.hp.MaHocPhi || !$scope.hp.MaHocSinh) {
            alert("Vui lòng nhập Mã phiếu và Mã học sinh!");
            return;
        }

        // 2. Xử lý kiểu dữ liệu Số (Tránh lỗi string gửi lên server)
        // Chuyển Tháng và Tiền thành số thực sự
        $scope.hp.ThangThu = Number($scope.hp.ThangThu);
        $scope.hp.TongTien = Number($scope.hp.TongTien);

        // 3. Xử lý logic Trạng thái & Ngày thanh toán
        if (!$scope.hp.TrangThai) {
            $scope.hp.TrangThai = "Chưa đóng";
        }

        // Nếu là "Chưa đóng" -> Bắt buộc Ngày thanh toán phải là NULL
        if ($scope.hp.TrangThai === "Chưa đóng") {
            $scope.hp.NgayThanhToan = null;
            $scope.hp.HinhThuc = null; // Chưa đóng thì chưa có hình thức trả
        } else {
            // Nếu "Đã đóng" mà quên chọn ngày -> Tự lấy ngày giờ hiện tại
            if (!$scope.hp.NgayThanhToan) {
                $scope.hp.NgayThanhToan = new Date(); 
            }
        }

        // 4. In ra Console để kiểm tra trước khi gửi (F12 để xem)
        console.log("Dữ liệu gửi đi:", $scope.hp);

        // 5. Gửi API
        $http.post(apiBase + "/create", $scope.hp)
        .then(function(res) {
            alert("✅ Tạo phiếu thu thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { 
            // In lỗi chi tiết ra console
            console.error("Lỗi API:", err);
            
            if(err.status === 500) {
                alert("❌ Lỗi Server! Có thể do trùng Mã Học Phí hoặc Mã Học Sinh không tồn tại.");
            } else {
                alert("❌ Lỗi: " + (err.data || err.statusText)); 
            }
        });
    };

    // 4. CẬP NHẬT / THANH TOÁN (Dùng API /pay theo Swagger)
    $scope.capNhat = function() {
        // API Sửa của bạn tên là /pay
        $http.post(apiBase + "/pay", $scope.hp)
        .then(function(res) {
            alert("Cập nhật thành công!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { alert("Lỗi: " + err.data); });
    };

    // 5. XÓA
    $scope.xoa = function() {
        if (!confirm("Bạn chắc chắn muốn xóa phiếu thu này?")) return;
        
        var id = $scope.hp.MaHocPhi;
        $http.post(apiBase + "/delete?id=" + id)
        .then(function(res) {
            alert("Đã xóa phiếu thu!");
            $scope.layDanhSach();
            $scope.lamMoi();
        }, function(err) { alert("Lỗi: " + err.data); });
    };

    // 6. LÀM MỚI
    $scope.lamMoi = function() {
        $scope.hp = {};
        $scope.cheDoSua = false;
    };
});