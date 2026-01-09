var app = angular.module('AppHocSinh', []);

app.controller('HocSinhController', function($scope, $http) {

    // --- CẤU HÌNH API ---
    var host = "http://localhost:6002"; // Thay đổi port nếu cần
    var apiHocSinh = host + "/api/HocSinh"; 

    $scope.dsHocSinh = [];
    $scope.isEdit = false; // Cờ xác định đang thêm mới hay sửa

    // Khởi tạo đối tượng học sinh
    $scope.hs = {
        MaHocSinh: "",
        HoTen: "",
        NgaySinh: null,
        GioiTinh: "Nam",
        DiaChi: "",
        MaLop: "MAM01", // Mặc định lớp
        PhuHuynh: "",
        SDTLienHe: ""
    };

    // --- 1. LẤY DANH SÁCH (GET) ---
    $scope.loadData = function() {
        $http.get(apiHocSinh + "/get-all").then(function(res) {
            $scope.dsHocSinh = res.data;
        }, function(err) {
            console.error("Lỗi tải dữ liệu:", err);
        });
    };

    // --- 2. LƯU DỮ LIỆU (CREATE / UPDATE) ---
    $scope.luuHocSinh = function() {
        // Validate đơn giản
        if (!$scope.hs.MaHocSinh || !$scope.hs.HoTen) {
            alert("Vui lòng nhập Mã và Tên học sinh!");
            return;
        }

        if ($scope.isEdit) {
            // --- CẬP NHẬT (UPDATE) ---
            $http.post(apiHocSinh + "/update", $scope.hs).then(function(res) {
                alert("Đã cập nhật thành công!");
                $scope.loadData();
                $scope.lamMoi();
            }, function(err) {
                alert("Lỗi cập nhật: " + err.data);
            });
        } else {
            // --- THÊM MỚI (CREATE) ---
            $http.post(apiHocSinh + "/create", $scope.hs).then(function(res) {
                // Backend trả về chuỗi thông báo, nếu rỗng là thành công
                if(res.data && res.data.length > 0) {
                     alert("Lỗi: " + res.data); // Ví dụ: Trùng mã
                } else {
                     alert("Thêm mới thành công!");
                     $scope.loadData();
                     $scope.lamMoi();
                }
            }, function(err) {
                alert("Lỗi thêm mới: " + err.data);
            });
        }
    };

    // --- 3. CHỌN ĐỂ SỬA ---
    $scope.chonSua = function(item) {
        $scope.isEdit = true; // Chuyển sang chế độ sửa
        
        // Copy dữ liệu để tránh sửa trực tiếp trên bảng khi chưa lưu
        $scope.hs = angular.copy(item);

        // Xử lý Ngày Sinh: API trả về chuỗi, cần chuyển thành Date object để hiện lên input
        if ($scope.hs.NgaySinh) {
            $scope.hs.NgaySinh = new Date($scope.hs.NgaySinh);
        }
    };

    // --- 4. XÓA HỌC SINH ---
    $scope.xoaHocSinh = function(id) {
        if (!confirm("Bạn có chắc muốn xóa học sinh có mã: " + id + "?")) return;

        $http.post(apiHocSinh + "/delete?id=" + id).then(function(res) {
            alert("Đã xóa thành công!");
            $scope.loadData();
            $scope.lamMoi(); // Nếu đang chọn sửa thằng bị xóa thì clear đi
        }, function(err) {
            alert("Không thể xóa. Có thể dữ liệu đang được sử dụng ở bảng khác.");
        });
    };

    // --- 5. LÀM MỚI FORM ---
    $scope.lamMoi = function() {
        $scope.isEdit = false;
        $scope.hs = {
            MaHocSinh: "",
            HoTen: "",
            NgaySinh: null,
            GioiTinh: "Nam",
            DiaChi: "",
            MaLop: "MAM01",
            PhuHuynh: "",
            SDTLienHe: ""
        };
    };

    // Chạy khi tải trang
    $scope.loadData();
});